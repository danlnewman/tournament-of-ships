using System;
using System.Collections;
using System.Collections.Generic;

namespace server.Data
{
    public class ClientMessage 
    {
        public int client { get; set; }
        public string[] commands { get; set; }
    }
}