using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverWithMetaData.Services
{
    public class DriverService: Driver.DriverBase
    {
        private readonly ILogger<DriverService> _logger;
        public DriverService(ILogger<DriverService> logger) 
        {
            _logger = logger;
        }

        public override Task<GetStatusReply> GetDriverStatus(GetDriverStatusRequest request, ServerCallContext context)
        {
            bool status = true;

            var statusDetail = new Status()
            {
                Id = 0,
                Status_ = "The Hello World Machine is running properly."
            };
            var reply = new GetStatusReply { 
                Status = status,
                Details = statusDetail
            };

            return Task.FromResult(reply);
        }

        public override Task<GetExceptionsReply> GetExceptions(GetExceptionsRequest request, ServerCallContext context)
        {
            if(request.Equipment == null)
            {
                Console.WriteLine("No equipment was provided");
                _logger.LogError("Equipment is required");
            }

            var reply = new GetExceptionsReply {};
            var exception = CheckForErrors(request.Equipment, request.FromTimestamp);
            reply.Exceptions.Add(exception);

            return Task.FromResult(reply);
        }

        public override Task<GetStatusReply> GetEquipmentStatus(GetEquipmentStatusRequest request, ServerCallContext context)
        {
            var equipmentId = request.Equipment.Id;
            var equipmentIP = request.Equipment.IpAddress;

            string statusMessage = string.Empty;
            var status = CheckResponseFromEquipment(equipmentId);
            if (status)
            {
                statusMessage = "Everything seems to be working properly";
            }
            else
            {
                statusMessage = "Cannot communicate with the equipment";
            }

            var statusDetail = new Status
            {
                Id = equipmentId,
                Status_ = statusMessage
            };

            var reply = new GetStatusReply
            {
                Status = status,
                Details = statusDetail
            };

            return Task.FromResult(reply);
        }

        public override Task<GetParameterDataReply> GetParameterData(GetParameterDataRequest request, ServerCallContext context)
        {
            var dataList = new ParameterData();
            var reply = new GetParameterDataReply();
            reply.ParameterData.Add(dataList);

            return Task.FromResult(reply);
        }

        public override Task<GetParameterDataReply> GetParameterDataSet(GetParameterDataSetRequest request, ServerCallContext context)
        {
            var dataList = Array.Empty<ParameterData>();
            if(request.ParameterSetKey == "AllData")
            {
                dataList = GetAllData(request.Equipment);
            }
            else if(request.ParameterSetKey == "TemperatureData")
            {
                dataList = GetCurrentLaserTemp(request.Equipment);
            }

            var reply = new GetParameterDataReply();
            reply.ParameterData.Add(dataList);

            return Task.FromResult(reply);
        }

        private ParameterData[] GetAllData(Equipment equipment)
        {
            return GetCurrentLaserTemp(equipment);
        }

        private ParameterData[] GetCurrentLaserTemp(Equipment equipment)
        {
            ParameterData[] dataList = Array.Empty<ParameterData>();

            Random random = new Random(DateTime.Now.Millisecond);
            var tempt = 1500 + random.Next(101);

            var data = new ParameterData
            {
                EquipmentId= equipment.Id,
		        ParameterKey = "current_laser_temperature", // This is the parameter key defined in the manifest.yml file in the "parameters" section
		        Timestamp = DateTime.Now.ToString("2006-01-02 03:04:05"),
		        Value =  tempt.ToString()
            };

            dataList.Append(data);

            return dataList;
        }

        private bool CheckResponseFromEquipment(uint equipmentId)
        {
            return true;
        }

        private Exception CheckForErrors(Equipment equipment, string fromTimestamp)
        {
            var exception = new Exception
            {
                EquipmentId = equipment.Id,
                Severity=    "info",
		        ExceptionId = 12416, // Numerical identifier of exception generally defined by equipment manufacturer
		        Name = "TutorialException",
		        Description = "This is an example exception.",
		        RecordedAt = DateTime.Now.ToString("2006-01-02 03:04:05")
            };

            return exception;
        }
    }
}
