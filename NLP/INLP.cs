using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP
{
    interface INLP
    {
        Dictionary<string, Type> Parameter { get; }

        Dictionary<string, object> Config { set; }


    }
}
