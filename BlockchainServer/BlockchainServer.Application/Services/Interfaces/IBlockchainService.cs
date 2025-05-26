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
        void AddBlock(EdgeDeviceData data);
        List<Block> GetBlockchain();
    }
}
