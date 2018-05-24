﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsds.DAL.Entities;
using Wsds.DAL.Entities.Communication;
using Wsds.DAL.Infrastructure;

namespace Wsds.DAL.Repository.Abstract
{
    public interface ICartRepository
    {
        IEnumerable<ClientOrderProduct_DTO> GetClientOrderProductsByUserId(long userId);
        IEnumerable<ClientOrderProduct_DTO> GetClientOrderProductsByClietId(long clientId);
        SCNMethodResult<ClientOrderProduct_DTO> UpdateCartProduct(ClientOrderProduct_DTO item,long clientId);
        ClientOrderProduct_DTO InsertCartProduct(ClientOrderProduct_DTO item,long clientId,long currency, long idApp);
        ClientOrder_DTO GetOrCreateClientDraftOrder(long clientId,long currencyId,long idApp);
        ClientOrder_DTO SaveClientOrder(ClientOrder_DTO order,long clientId);
        void DeleteCartProduct(long id,long clientId);
        IEnumerable<ClientOrderProduct_DTO> GetClientOrderProductsByOrderId(long orderId);
        IEnumerable<ClientOrderProduct_DTO> GetClientHistOrderProductsByOrderId(long orderId);

        IEnumerable<ClientOrder_DTO> GetClientOrders(long clientId);
        IEnumerable<CalculateCartResponse> CalculateCart(CalculateCartRequest cartObj, long card, long clientId, long currency,long idApp);
        PostOrderResponse PostOrder(ClientOrder_DTO order);
        IEnumerable<ClientOrderProductsByDate_DTO> GetOrderProductsByDate(string datesRange, long clientId);
        //ClientOrder_DTO GetClientOrder(long orderId, long idClient);
        ClientOrder_DTO GetClientHistOrder(long orderId, long idClient);
        IEnumerable<Shipment_DTO> GenerateShipments(long clientId, long currencyId,long idApp);
        Shipment_DTO SaveShipment(Shipment_DTO shipment, long idClient);
    }
}
