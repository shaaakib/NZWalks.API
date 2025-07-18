﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository.Interface
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
