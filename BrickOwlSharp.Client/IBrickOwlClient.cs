﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrickOwlSharp.Client
{
    public interface IBrickOwlClient
    {
        Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken = default);
        Task<List<Wishlist>> GetWishlistsAsync(CancellationToken cancellationToken = default);
        Task CreateInventoryAsync(NewInventory newInventory, CancellationToken cancellationToken = default);
    }       
}