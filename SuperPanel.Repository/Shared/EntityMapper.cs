using AutoMapper;
using SuperPanel.DAL.Entities;
using SuperPanel.Dto.EntityDto;
using SuperPanel.Repository.Interfaces;

namespace SuperPanel.Repository.Shared
{
    public class EntityMapper : IEntityMapper
    {
        private MapperConfiguration? _config;

        private IMapper? _mapper;

        public EntityMapper()
        {
            Configure();
            Create();
        }

        private void Configure()
        {
            _config ??= new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>().ReverseMap();
            });
        }

        private void Create()
        {
            if (_mapper == null && _config != null)
            {
                _mapper = _config.CreateMapper();
            }
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
