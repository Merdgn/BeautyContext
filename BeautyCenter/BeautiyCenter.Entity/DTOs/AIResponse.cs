using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.Entity.DTOs
{
    public class Shape
    {
        public string ClassName { get; set; } = string.Empty;
        public string Probability { get; set; } = string.Empty;
    }

    public class AIResponseData
    {
        public List<Shape> Shapes { get; set; } = new List<Shape>();
        public List<string> Suggestions { get; set; } = new List<string>();
    }

    public class AIResponse
    {
        public bool Success { get; set; }
        public AIResponseData Data { get; set; } = new AIResponseData();
    }
}
