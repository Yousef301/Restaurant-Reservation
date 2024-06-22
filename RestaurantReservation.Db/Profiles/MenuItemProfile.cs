using AutoMapper;
using RestaurantReservation.Db.DTOs;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Profiles;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItem, MenuItemDto>();
        CreateMap<MenuItemDto, MenuItem>();
    }
}