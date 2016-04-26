using OdeToFood.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace OdeToFood.Services
{

    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        void Add(Restaurant newRestaurant);
        int Commit();
    }

    public class SqlRestuarantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestuarantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public void Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            //_context.SaveChanges();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id.Equals(id));
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
    }

    public class InMemoryRestaurantData : IRestaurantData
    {

        static List<Restaurant> _restaurants;

        static InMemoryRestaurantData()
        {
            _restaurants = new   List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Bambino's" },
                new Restaurant { Id = 2, Name = "Brew Co." },
                new Restaurant { Id = 3, Name = "Bruno's" }
            };
        }

        public void Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(newRestaurant);
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id.Equals(id));
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }
    }
}
