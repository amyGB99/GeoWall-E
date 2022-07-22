using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.Ast
{
  public abstract  class AstNode
    {
        #region semantica
        public abstract bool ChequeoSemantico(Scope scope,List<CompilingError> listaerrores);
        #endregion
    }
}
