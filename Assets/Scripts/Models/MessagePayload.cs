using System.Collections.Generic;

namespace MessageModel
{
    [System.Serializable]
    public class MessagePayload
    {
        public string role; // "user" or "system"
        public string content;
        // public List<MessageContent> Content { get; set; } = new List<MessageContent>();
    }

    // public interface IMessageContent
    // {
    //     string Type { get; }
    // }
    // [System.Serializable]
    // public class TextContent : IMessageContent
    // {
    //     public string Type { get; } = "text";
    //     public string Value { get; set; }
    // }

    // [System.Serializable]
    // public class ImageContent : IMessageContent
    // {
    //     public string Type { get; } = "image_url";
    //     public string Url { get; set; }
    //     public string Detail { get; set; } = "auto"; // default to auto if not specified
    // }
    // [System.Serializable]
    // public class MessageContent
    // {
    //     public string type { get; set; }
    //     public string text { get; set; }
    //     public string? imageUrl { get; set; } // URL for image content
    //     public string? imageDetail { get; set; } // Detail level for images

    // }

    // public class TextContent
    // {
    //     public string value { get; set; }
    // }

    // public class ImageUrl
    // {
    //     public string Url { get; set; } // Required, external URL of the image
    //     public string Detail { get; set; } // Optional, can be "low", "high", or "auto"
    // }
    
}