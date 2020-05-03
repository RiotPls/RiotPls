using System;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    /// <summary>
    ///     Represents information about images and splash art for a champion.
    /// </summary>
    public sealed class StaticImage
    {
        // TODO: Implement URL helpers for champion images.

        /// <summary>
        ///     The name of the image for the full splash art for the champion.
        /// </summary>
        public string Full { get; }
        
        /// <summary>
        ///     The name of the image for the sprite of the champion.
        /// </summary>
        public string Sprite { get; }
        
        /// <summary>
        ///     The group of the champion.
        /// </summary>
        public ImageGroup Group { get; }
        
        public int X { get; }
        
        public int Y { get; }
        
        /// <summary>
        ///     Width of the image.
        /// </summary>
        public int W { get; }
        
        /// <summary>
        ///     Height of the image.
        /// </summary>
        public int H { get; }

        internal StaticImage(StaticImageDto dto)
        {
            Full = dto.Full;
            Sprite = dto.Sprite;
            Group = Enum.Parse<ImageGroup>(dto.Group, true);
            X = dto.X;
            Y = dto.Y;
            W = dto.W;
            H = dto.H;
        }
    }
}