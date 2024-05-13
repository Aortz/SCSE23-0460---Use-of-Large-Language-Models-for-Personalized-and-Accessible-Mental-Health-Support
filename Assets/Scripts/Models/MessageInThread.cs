using System;
using System.Collections.Generic;

namespace MessageModel
{
    [System.Serializable]
    public class MessagesThreadResponse
    {
        public string @object; // Use @ to allow using reserved keyword 'object'
        public List<MessageResponse> data;
        public string first_id;
        public string last_id;
        public bool has_more;
    }
}