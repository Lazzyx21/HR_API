using HR_API.Models;
using HR_API.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace HR_API.Services
{
    public class Recuriment_OnboardingServices : IRecuriment_OnboardingServices
    {
        private readonly ILogger<Recuriment_OnboardingServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly ErptestingContext _dbContext;

        public Recuriment_OnboardingServices(ILogger<Recuriment_OnboardingServices> logger, IConfiguration configuration, ErptestingContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        //Develop Job Posting System(create, edit, delete job posts).
        //Implement Candidate Tracking(store applications, resumes).
        //Automate Onboarding Process(document submission, approvals).

    }
}
