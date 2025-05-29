using BlockchainServer.Domain.Entities.Shared;
using BlockchainServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;


namespace BlockchainServer.Application.Services.Interfaces
{
    public interface IBlockchainService
    {
        Block GetLatestBlock();
        void AddBlock(EdgeDeviceDataShared data);
        List<Block> GetBlockchain();
        (bool IsValid, int? InvalidBlockIndex) VerifyBlockchain();
        (bool IsValid, int? InvalidBlockIndex) VerifyBlockchainSignatures();
    }
}
