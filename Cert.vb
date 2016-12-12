Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Public Class QueryTrustCertificatePolicy
    Implements System.Net.ICertificatePolicy

    Private Const CERT_E_UNTRUSTEDROOT As UInteger = &H800B0109UI

    Public Sub New()
    End Sub

    Public Function CheckValidationResult(ByVal srvPoint As System.Net.ServicePoint, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal request As System.Net.WebRequest, ByVal certificateProblem As Integer) As Boolean Implements System.Net.ICertificatePolicy.CheckValidationResult
        Return True
    End Function

End Class
