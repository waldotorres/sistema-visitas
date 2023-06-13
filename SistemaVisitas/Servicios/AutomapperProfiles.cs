using AutoMapper;
using SistemaVisitas.DTO;
using SistemaVisitas.Models;

namespace SistemaVisitas.Servicios
{
	public class AutomapperProfiles:Profile
	{
        public AutomapperProfiles()
        {
            CreateMap<Ciudad, CiudadCreacionDTO>().ReverseMap();
			CreateMap<Local, LocalCreacionDTO>().ReverseMap();
			CreateMap<CitaCreacionDTO, Cita>().ReverseMap();
			CreateMap<VisitaCreacionDTO, Visita>().ReverseMap();
			CreateMap<Usuario, UsuarioCreacionDTO>().ReverseMap();
		}
    }
}
