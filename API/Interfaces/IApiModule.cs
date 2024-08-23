// <copyright file="IApiModule.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Interfaces
{
    public interface IApiModule
    {
        public void MapEndpoint(WebApplication app);
    }
}