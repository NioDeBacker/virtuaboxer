using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtuaBoxer.Shared.Boxers;

public interface IBoxerService
{
    Task<BoxerResponse.GetIndex> GetIndexAsync(BoxerRequest.GetIndex request);
    Task<BoxerDto.Detail> GetDetailAsync(int boxerId);
    Task DeleteAsync(int boxerId);
    Task<int> CreateAsync(BoxerDto.Mutate model);
    Task EditAsync(int boxerId, BoxerDto.Mutate model);
}
