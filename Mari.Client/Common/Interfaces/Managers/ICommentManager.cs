﻿// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Mari.Client.Models.Releases;
using Mari.Contracts.Comments.Responce;
using Mari.Contracts.Releases.Responce;

namespace Mari.Client.Common.Interfaces.Managers;

public interface ICommentManager
{
    Task Create(CommentResponse comment, CancellationToken token);
    Task<IEnumerable<CommentResponse>> GetAll(Guid releaseId,CancellationToken token);
    Task UpdateComments(CommentResponse comment,CancellationToken token);
    Task DeleteComments(Guid commmentId, CancellationToken token);
}
