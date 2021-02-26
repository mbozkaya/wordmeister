﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Helpers
{
    public class Enums
    {
        public enum UploadFileType
        {
            ProfilePic = 1,
        }

        public enum DynamicConditions
        {
            Equal = 1,
            NotEqual = 2,
            LessThan = 3,
            LessThanOrEqual = 4,
            GreaterThan = 5,
            GreaterThanOrEqual = 6
        }
    }
}
