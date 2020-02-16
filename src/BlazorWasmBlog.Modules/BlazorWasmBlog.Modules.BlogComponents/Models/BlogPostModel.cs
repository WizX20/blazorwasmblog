using System;

namespace BlazorWasmBlog.Modules.BlogComponents.Models
{
    public class BlogPostModel
    {
        public string Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string IntroductionText { get; set; }

        public string Text { get; set; }

        public int ReadingMinutes { get; set; }

        public string TeaserImage { get; set; }

        public string MainImage { get; set; }

        public DateTime PublishedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
