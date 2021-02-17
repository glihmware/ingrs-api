
namespace Ingrs
{
    /// <summary>
    ///   Ingrs reasons.
    /// </summary>
    public enum IngrsReason
    {
        Ok,

        RsaPubInvalid,
        RsaPrvInvalid,
        EcdsaPubInvalid,
        EcdsaPrvInvalid,
        EncryptionError,
        DecryptionError,

        UserNameAlreadyExists,
        UserBadCredencials,
        UserUnknown,

        DomainNameAlreadyExists,
        DomainUnknown,

        EFCtxError,
    }





}
