using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ThemeFixer
{
    public partial class MainWindow : Form
    {
        private const string WRITER_REGISTRY_KEY = @"Software\OpenLiveWriter\Weblogs";
        private const string BODY_TOKEN = "{post-body}";
        private const string TITLE_TOKEN = "{post-title}";
        private Regex CSS_URLS = new Regex(@"url\s*\((.*?)\)", RegexOptions.Compiled);

        public MainWindow()
        {
            InitializeComponent();

            LoadBlogs();
        }

        private void LoadBlogs()
        {
            // Read all of the user's weblogs out of the registery and load them into the combobox
            using (RegistryKey weblogs = Registry.CurrentUser.OpenSubKey(WRITER_REGISTRY_KEY))
            {
                string[] weblogIds = weblogs.GetSubKeyNames();
                foreach (string id in weblogIds)
                {
                    try
                    {
                        using (RegistryKey weblog = Registry.CurrentUser.OpenSubKey(String.Format("{0}\\{1}", WRITER_REGISTRY_KEY, id)))
                        {
                            try
                            {
                                using (RegistryKey template = Registry.CurrentUser.OpenSubKey(String.Format("{0}\\{1}\\EditorTemplate\\templates", WRITER_REGISTRY_KEY, id)))
                                {
                                    if (template != null)
                                    {
                                        Blog blog = new Blog();
                                        try
                                        {
                                            blog.Name = weblog.GetValue("BlogName") as string;
                                            blog.Url = weblog.GetValue("HomepageUrl") as string;
                                            blog.Id = id;
                                        }
                                        catch (Exception ex3)
                                        {
                                            //MessageBox.Show(string.Format("Exception3: {0}", ex3.Message));
                                        }
                                        string appDateFolder = string.Empty;

                                        appDateFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                        blog.TemplateDirectory = String.Format(@"{0}\OpenLiveWriter\blogtemplates\{1}\", appDateFolder, id);
                                        try
                                        {
                                            blog.PreviewPath = appDateFolder + @"\OpenLiveWriter\blogtemplates\" + id + "\\" + template.GetValue("Webpage", "") as string;
                                            blog.WysiwygPath = appDateFolder + @"\OpenLiveWriter\blogtemplates\" + id + "\\" + template.GetValue("Framed", "") as string;
                                        }
                                        catch (Exception ex2)
                                        {
                                            MessageBox.Show(string.Format("Exception2: {0}", ex2.Message));
                                        }
                                        comboBoxBlogs.Items.Add(blog);
                                    }
                                }
                            }
                            catch (Exception ex1)
                            {
                                //MessageBox.Show(string.Format("Exception1: {0}", ex1.Message));
                            }
                        }
                    }
                    catch (Exception ex0)
                    {
                        //MessageBox.Show(string.Format("Exception0: {0}", ex0.Message));
                    }
                }
            }

            // We need to get a callback when the document loads so we can attach the click and mouse over
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);

            // Change the SelectedIndex to force the first blog to be loaded into CurrentBlog
            comboBoxBlogs.SelectedIndexChanged += new EventHandler(comboBoxBlogs_SelectedIndexChanged);
            comboBoxBlogs.SelectedIndex = 0;
        }

        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Ignore any updates where the document isn't actually ready
            if (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                return;

            webBrowser.Document.MouseOver += new HtmlElementEventHandler(Document_MouseOver);
            webBrowser.Document.Click += new HtmlElementEventHandler(Document_Click);
        }

        private HtmlElement TitleElement;
        private HtmlElement BodyElement;
        private HtmlElement _activeElement;
        private string _activeElementStyle;
        private Blog CurrentBlog { get; set; }
        private SelectionState ActionState = SelectionState.None;

        private enum SelectionState
        {
            None,
            Title,
            Body
        }


        void Document_Click(object sender, HtmlElementEventArgs e)
        {
            // The user hasn't started the element picking process yet, 
            // just let the click through
            if (ActionState == SelectionState.None)
                return;

            // Set back thhe orginal style before we put the hover style on
            if(_activeElement != null)
                _activeElement.Style = _activeElementStyle;

            buttonNavigate.Enabled = false;

            // Clear out the current element we are hovering
            _activeElement = null;

            // Put the title token in the clicked element
            if (ActionState == SelectionState.Title)
            {
                HtmlElement ele = webBrowser.Document.GetElementFromPoint(e.MousePosition);
                ele.InnerHtml = "{post-title}";
                TitleElement = ele;
                labelStatus.Text = "When ready click the 'Pick Body' button.";
            }

            // Put the body token in the clicked element
            if (ActionState == SelectionState.Body)
            {
                HtmlElement ele = webBrowser.Document.GetElementFromPoint(e.MousePosition);
                BodyElement = ele;

                string titleHtml = "";

                // Look to see if the title is inside of the body element
                foreach (HtmlElement innerElements in BodyElement.Children)
                {
                    if (innerElements.InnerHtml == TITLE_TOKEN)
                    {
                        // Save the element that contains the title, we will need to
                        // make sure it ends up in the body element
                        titleHtml = TitleElement.OuterHtml;
                    }
                }

                // Set the body element to have the title if we found it
                // and the body token
                BodyElement.InnerHtml = titleHtml + BODY_TOKEN;

                // Enable the Save button
                buttonSave.Enabled = true;

                labelStatus.Text = "Now that the title and body have been replaced, click save to generate the template.";
            }

            // Eat the click
            e.BubbleEvent = true;

            // Reset the user action
            ActionState = SelectionState.None;
            
        }

        void Document_MouseOver(object sender, HtmlElementEventArgs e)
        {
            // Swap out the last element we hovered back to its orginal style
            if (_activeElement != null)
            {
                _activeElement.Style = _activeElementStyle;
                _activeElement = null;
            }

            // If the user hasn't started selecting anything yet, just ignore the hover
            if (ActionState == SelectionState.None)
                return;

            // Don't let javascript get the hover event
            e.BubbleEvent = false;

            // Find the element that is bring hovered and set its style so it appears highlighted
            HtmlElement ele = webBrowser.Document.GetElementFromPoint(e.MousePosition);
            _activeElementStyle = ele.Style;
            _activeElement = ele;
            ele.Style = "color: red; background-color: black";
        }

        
        void comboBoxBlogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Set the current blog based on what is in the combobox
                CurrentBlog = comboBoxBlogs.SelectedItem as Blog;
                textBoxUrl.Text = CurrentBlog.Url;
                NavigateBrowser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading blogs: {0}", ex.Message));
            }
        }

        private void NavigateBrowser()
        {
            string url = textBoxUrl.Text;

            if (string.IsNullOrEmpty(url.Trim()))
                return;

            if (!url.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
                url = "http://" + url;

            webBrowser.Navigate(new Uri(url));
        }

        private class Blog
        {
            public Blog()
            {

            }

            public string Id { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
            public string WysiwygPath { get; set; }
            public string PreviewPath { get; set; }
            public string TemplateDirectory { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private void buttonNavigate_Click(object sender, EventArgs e)
        {
            NavigateBrowser();
        }

        private void buttonSelectTitle_Click(object sender, EventArgs e)
        {
            ActionState = SelectionState.Title;
            buttonSelectTitle.Enabled = false;
            buttonSelectBody.Enabled = true;
            labelStatus.Text = "Select the element that shows your blog post title.";
        }

        private void buttonSelectBody_Click(object sender, EventArgs e)
        {
            ActionState = SelectionState.Body;
            buttonSelectBody.Enabled = false;
            labelStatus.Text = "Select the element that shows your blog post body.";
        }        

        private void buttonSave_Click(object sender, EventArgs e)
        {
            buttonSave.Enabled = false;
            labelStatus.Text = "Downloading all the images for your template... please wait";
            this.Refresh();

            // Download all the images on the page
            foreach (HtmlElement ele in webBrowser.Document.Images)
            {
                string url = "";
                try
                {
                    url = ele.GetAttribute("src");
                    url = new Uri(webBrowser.Document.Url, url).AbsoluteUri;
                    string local = CurrentBlog.TemplateDirectory + Guid.NewGuid().ToString() + url.Substring(url.LastIndexOf("."));
                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadFile(url, local);
                    }
                    ele.SetAttribute("src", new Uri(local).AbsoluteUri);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error while downloading an image: " + url + Environment.NewLine + ex.ToString());
                }
            }

            // Download all of the CSS files on the page
            foreach (HtmlElement ele in webBrowser.Document.All)
            {
                try
                {
                    if (ele.TagName == "LINK")
                    {                        
                        string cssFileUrl = new Uri(webBrowser.Document.Url, ele.GetAttribute("href")).AbsoluteUri;

                        if(cssFileUrl.EndsWith(".css", StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Download the remote file
                            string cssLocalFile = CurrentBlog.TemplateDirectory + Guid.NewGuid().ToString() + cssFileUrl.Substring(cssFileUrl.LastIndexOf("."));
                            using (WebClient wc = new WebClient())
                            {
                                wc.DownloadFile(cssFileUrl, cssLocalFile);
                            }

                            // Swap out the remote path with the local path
                            ele.SetAttribute("href", new Uri(cssLocalFile).AbsoluteUri);

                            // Look for all the 'url(..)' inside of the CSS file
                            // We download these because they could be background images
                            string css = File.ReadAllText(cssLocalFile);                            
                            MatchCollection mc = CSS_URLS.Matches(css);
                            foreach(Match m in mc)
                            {
                                string cssImage = new Uri(new Uri(cssFileUrl), m.Groups[1].Value).AbsoluteUri;
                                string localCssImage = CurrentBlog.TemplateDirectory + Guid.NewGuid().ToString() + cssImage.Substring(cssImage.LastIndexOf("."));

                                try
                                {
                                    using (WebClient wc = new WebClient())
                                    {
                                        wc.DownloadFile(cssImage, localCssImage);
                                    }
                                    css = css.Replace(m.Groups[1].Value, new Uri(localCssImage).AbsoluteUri);
                                }
                                catch (Exception)
                                {

                                }
                            }

                            File.WriteAllText(cssLocalFile, css);
                        }
                        
                    }
                }
                catch (Exception)
                {

                }
            }


            // Writer out the full html for the preview tab
            File.WriteAllText(CurrentBlog.PreviewPath, webBrowser.Document.Body.Parent.OuterHtml);

            // Walk up the parents from the body token
            // Removing all the siblings 
            HtmlElement currentElement = BodyElement;
            while (currentElement.TagName != "BODY")
            {
                HtmlElement parentElement = currentElement.Parent;
                foreach (HtmlElement child in parentElement.Children)
                {
                    // Don't clear out the title element
                    if (child.InnerHtml != null && child.InnerHtml.Contains(TITLE_TOKEN))
                        continue;

                    if (child != currentElement)
                    {
                        try
                        {
                            child.OuterHtml = "";
                        }
                        catch (Exception)
                        {
                            try
                            {
                                child.InnerHtml = "";
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                currentElement = currentElement.Parent;
            }
            
            // Write out the html for the 'edit' tab
            File.WriteAllText(CurrentBlog.WysiwygPath, webBrowser.Document.Body.Parent.OuterHtml);

            labelStatus.Text = "All done, open Windows Live Writer to see if your template looks better.";

        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(CurrentBlog.TemplateDirectory);
        }

        private void textBoxUrl_Enter(object sender, EventArgs e)
        {
            textBoxUrl.SelectAll();
        }
    }
}
