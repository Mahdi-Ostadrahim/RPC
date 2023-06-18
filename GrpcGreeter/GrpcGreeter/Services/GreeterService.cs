using Grpc.Core;
using GrpcGreeter;
using GrpcGreeter.Models;
using GrpcGreeter.Services;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        //private Database Db = new Database();
        private WorkersService _workersservice;
        public GreeterService(ILogger<GreeterService> logger)
        {
            BsonClassMap.RegisterClassMap<Worker>();
            BsonClassMap.RegisterClassMap<Human>();
            _logger = logger;
            _workersservice = new WorkersService("mongodb://localhost:27017", "WorkersList", "Workers");
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
                {
                    Message = "User doesn't exist"
                });
        }
        public override async Task<GetReply> GetPerson(GetRequest request, ServerCallContext context)
        {
            Worker a = await _workersservice.GetAsync(request.NationalID);
            if (a == null)
            {
                return new GetReply
                {
                    Message = "User doesn't exist"
                };
            }
            return new GetReply
            {
                Message = a.GetPerson()
            };
        }
        public override async Task<CreateReply> CreatePerson(CreateRequest request, ServerCallContext context)
        {
            Worker a = new Worker(request.Name, request.Age, request.NationalID, request.Male, false);
            await _workersservice.CreateAsync(a);
            return new CreateReply
            {
                Message = "Person Created Successfully"
            };
        }
        public override async Task<WorkerReply> CreateWorker(WorkerRequest request, ServerCallContext context)
        {
            Worker a = new Worker(request.Name, request.Age, request.NationalID, request.Male, request.Employed, request.Experience, request.Company, request.WorkerID);
            await _workersservice.CreateAsync(a);
            return new WorkerReply
            {
                Message = "Worker Created Successfully"
            };
        }
        public override async Task<RemoveReply> RemovePerson(RemoveRequest request, ServerCallContext context)
        {
            await _workersservice.RemoveAsync(request.NationalID);
            return new RemoveReply
            {
                Message = "Person Removed Successfully"
            };
        }
        public override async Task<StartReply> StartPerson(StartRequest request, ServerCallContext context)
        {
            Worker a = await _workersservice.GetAsync(request.NationalID);
            a.SetEmployment(request.Company, request.WorkerID);
            await _workersservice.UpdateAsync(request.NationalID, a);
            return new StartReply
            {
                Message = a.GetName() + " started working at " + request.Company + $" with working ID of {request.WorkerID}"
            };
        }
        public override async Task<EndReply> EndPerson(EndRequest request, ServerCallContext context)
        {
            Worker a = await _workersservice.GetAsync(request.NationalID);
            a.EndofEmployment();
            await _workersservice.UpdateAsync(request.NationalID, a);
            return new EndReply
            {
                Message = a.GetName() + " is no longer working"
            };
        }
        public override async Task<ExpReply> AddExp(ExpRequest request, ServerCallContext context)
        {
            Worker a = await _workersservice.GetAsync(request.NationalID);
            a.SetExperience(request.Value);
            await _workersservice.UpdateAsync(request.NationalID, a);
            return new ExpReply
            {
                Message = a.GetName() + $" has {request.Value} years of experience"
            };
        }
    }
}