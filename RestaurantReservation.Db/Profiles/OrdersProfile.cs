using AutoMapper;
using RestaurantReservation.Db.DTOs;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Profiles;

public class OrdersProfile : Profile
{
    public OrdersProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();
    }
}