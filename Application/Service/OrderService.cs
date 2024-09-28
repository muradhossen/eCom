using Application.Common.Pagination;
using Application.Common.Result;
using Application.DTOs.Carts;
using Application.DTOs.Orders;
using Application.DTOs.Products;
using Application.Enums;
using Application.Errors;
using Application.RepositoryInterface;
using Application.Service.Base;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Application.Service
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository
            , ICartRepository cartRepository
            , IMapper mapper) : base(repository)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task<PagedList<Order>> GetOrdersAsync(OrderPageParam pageParam)
        {

            var query = _repository.TableNoTracking.AsQueryable();


            if (pageParam.IncludeDetails)
            {
                query = query.Include(c => c.OrderDetails);
            }

            if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
            {
                string searchKey = pageParam.SearchKey.ToLower().Trim();

                query = query
                    .Where(c => c.Code.ToLower().Contains(searchKey));
            }

            query = query.OrderByDescending(c => c.Id);

            return await PagedList<Order>.CreateAsync(query, pageParam.PageSize, pageParam.PageNumber);
        }
        public async Task<Result<Order>> AddByCartAsync(CartDto cartDto)
        {
            Cart cart = new Cart
            {
                OrderRequest = JsonSerializer.Serialize(cartDto)
            };

            if (await _cartRepository.AddAsync(cart))
            {
                var order = GetOrderFromCart(cartDto);
                bool isSavedOrder = await base.AddAsync(order);
                if (isSavedOrder)
                {
                    return Result<Order>.Success(order);
                }
                return Result<Order>.Failure(OrderError.CreateFailed);

            }
            return Result<Order>.Failure(OrderError.CartCreateFailed);
        }

        private Order GetOrderFromCart(CartDto cartDto)
        {
            Order order = new();
            order.Status = OrderStatus.Pending;
            order.TotalAmount = cartDto.GetTotalPrice();
            order.DiscountAmount = cartDto.GetTotalDiscount();
            order.ItemTotal = cartDto.GetItemsOriginaTotal();
            order.ItemTotal = cartDto.GetTotalPrice();

            OrderDetail orderDetail = new OrderDetail();
            orderDetail.ShippingAddress = cartDto.DeliveryAddress;
            orderDetail.Mobile = cartDto.Mobile;
            orderDetail.Type = DeliveryType.HomeDelivery; 

            foreach (var productContainer in cartDto.Items)
            {
                var discountItem = new DiscountItem();
                discountItem.ReferenceType = DiscountTypeReferences.Product;
                discountItem.ReferenceId = productContainer.Key.Id;
                discountItem.DiscountAmount = productContainer.GetItemDiscountPrice();
                discountItem.Type = productContainer.GetDiscountType(); 
                orderDetail.DiscountItems.Add(discountItem);

                var orderItem = new OrderItem();
                orderItem.Name = productContainer.Key.Name;
                orderItem.Price = productContainer.GetOriginalItemPrice();
                orderItem.CategoryId = productContainer.Key.SubCategoryId;  
                orderItem.SubCategoryId = productContainer.Key.SubCategoryId;
                orderItem.SectionName = productContainer.Key.Section.Name;
                orderItem.SectionId = productContainer.Key.Section.Id;
                orderItem.PricingItemId = productContainer.Key.Section.PricingItems.First().Id;
                orderDetail.Items.Add(orderItem);
            }

            order.OrderDetails = orderDetail;
            return order;
        }
    }
}
