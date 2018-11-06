

using Microsoft.ProjectOxford.Face.Contract;
using ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiosBoot.Models
{
   public class FaceObjct
    {
        public static Face MyFace { get; set; }
        public static IdentifiedPerson faceIdIdentification { get; set; }


        public static void Clear()
        {
             MyFace = null;
            faceIdIdentification = null;
        }
    }
}
