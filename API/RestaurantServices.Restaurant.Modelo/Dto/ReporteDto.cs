﻿using System;

namespace RestaurantServices.Restaurant.Modelo.Dto
{
    public class ReporteDto
    {
        public ReporteDto()
        {
            FechaDesde = DateTime.Now;
        }

        public int IdReporte { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }
}
