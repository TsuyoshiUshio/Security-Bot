using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class CreateReviewCommentContext
    {
        public Issue Issue { get; set; }
        public DecoratorContext DecoratorContext { get; set; }

    }
}
