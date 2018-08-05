using AppCore.ModelDTO;
using System.Collections.Generic;

namespace AppCore.Contexts
{
    public interface IPermohonanContext
    {
        permohonan GetPermohonan(StatusPermohonan status);
        permohonan GetLastPermohonan();
        IEnumerable<permohonan> GetPermohonans();
        bool CreatePermohonan(permohonan t);
    }
}