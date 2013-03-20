// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwfService.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services
{
    using FlashTools;

    using Orchard;

    public interface ISwfService : IDependency
    {
        void GetSwfFileDimensions(string fileServerPath, out int width, out int height);
    }

    public class SwfService : ISwfService
    {
        public SwfService(){}

        public void GetSwfFileDimensions(string fileServerPath,  out int width, out int height)
        {
            var file = new SWFFile(fileServerPath);
            width = file.FrameWidth / 20;
            height = file.FrameHeight / 20;
            /*
            var parser = new FlashHeaderReader(fileServerPath);
            width = parser.Width;
            height = parser.Height;*/
        }
    }
}