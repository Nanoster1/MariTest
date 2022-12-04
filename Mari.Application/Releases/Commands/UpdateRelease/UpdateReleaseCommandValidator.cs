using System.Data;
using FluentValidation;
using FluentValidation.Results;
using Mari.Domain.Releases.Enums;
using Mari.Domain.Releases.ValueObjects;

namespace Mari.Application.Releases.Commands.UpdateRelease;

public class UpdateReleaseCommandValidator : AbstractValidator<UpdateReleaseCommand>
{
    public UpdateReleaseCommandValidator(
        IValidator<ReleaseId> releaseIdValidator,
        IValidator<ReleaseCompleteDate> releaseCompleteDateValidator,
        IValidator<ReleaseDescription> releaseDescriptionValidator,
        IValidator<Issue> releaseIssueValidator,
        IValidator<PlatformName> releasePlatformNameValidator,
        IValidator<ReleaseStatus> releaseStatusValidator,
        IValidator<ReleaseVersion> releaseVersionValidator)
    {
        RuleFor(command => command.Id).SetValidator(releaseIdValidator);
        RuleFor(command => command.CompleteDate).SetValidator(releaseCompleteDateValidator);
        RuleFor(command => command.Description).SetValidator(releaseDescriptionValidator);
        RuleFor(command => command.MainIssue).SetValidator(releaseIssueValidator);
        RuleFor(command => command.PlatformName).SetValidator(releasePlatformNameValidator);
        RuleFor(command => command.Status).SetValidator(releaseStatusValidator);
        RuleFor(command => command.Version).SetValidator(releaseVersionValidator);
    }
}
