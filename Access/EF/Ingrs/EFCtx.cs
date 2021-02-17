using System;

namespace Ingrs.Access
{
    public static class EFCtx
    {
        public static DbCtx Ingrs;

        public static void Instanciate()
        {
            Ingrs = new DbCtx();
        }
    }
}
