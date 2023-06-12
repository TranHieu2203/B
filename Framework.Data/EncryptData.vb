Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class EncryptData
    Implements IDisposable

    'private static string plainText = "";    // original plaintext
    Private passPhrase As String = "Histaff"
    ' can be any string
    Private saltValue As String = "IportalTechcombank"
    ' can be any string
    Private hashAlgorithm As String = "SHA1"
    ' can be "MD5"
    Private passwordIterations As Integer = 2
    ' can be any number
    Private initVector As String = "@1B2c3D4e5F6g7H8"
    ' must be 16 bytes
    Private keySize As Integer = 256
    ' can be 192 or 128
    '
    ' SAMPLE: Symmetric key encryption and decryption using Rijndael algorithm.
    ' 
    ' To run this sample, create a new Visual C# project using the Console
    ' Application template and replace the contents of the Class1.cs file with
    ' the code below.
    '
    ' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
    ' EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
    ' WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
    ' 
    ' Copyright (C) 2002 Obviex(TM). All rights reserved.
    ' 


    ''' <summary>
    ''' This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
    ''' decrypt data. As long as encryption and decryption routines use the same
    ''' parameters to generate the keys, the keys are guaranteed to be the same.
    ''' The class uses static functions with duplicate code to make it easier to
    ''' demonstrate encryption and decryption logic. In a real-life application, 
    ''' this may not be the most efficient way of handling encryption, so - as
    ''' soon as you feel comfortable with it - you may want to redesign this class.
    ''' </summary>

    ''' <summary>
    ''' Encrypts specified plaintext using Rijndael symmetric key algorithm
    ''' and returns a base64-encoded result.
    ''' </summary>
    ''' <param name="plainText">
    ''' Plaintext value to be encrypted.
    ''' </param>
    ''' <param name="passPhrase">
    ''' Passphrase from which a pseudo-random password will be derived. The
    ''' derived password will be used to generate the encryption key.
    ''' Passphrase can be any string. In this example we assume that this
    ''' passphrase is an ASCII string.
    ''' </param>
    ''' <param name="saltValue">
    ''' Salt value used along with passphrase to generate password. Salt can
    ''' be any string. In this example we assume that salt is an ASCII string.
    ''' </param>
    ''' <param name="hashAlgorithm">
    ''' Hash algorithm used to generate password. Allowed values are: "MD5" and
    ''' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    ''' </param>
    ''' <param name="passwordIterations">
    ''' Number of iterations used to generate password. One or two iterations
    ''' should be enough.
    ''' </param>
    ''' <param name="initVector">
    ''' Initialization vector (or IV). This value is required to encrypt the
    ''' first block of plaintext data. For RijndaelManaged class IV must be 
    ''' exactly 16 ASCII characters long.
    ''' </param>
    ''' <param name="keySize">
    ''' Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    ''' Longer keys are more secure than shorter keys.
    ''' </param>
    ''' <returns>
    ''' Encrypted value formatted as a base64-encoded string.
    ''' </returns>
    Public Function EncryptString(ByVal plainText As String) As String
        Return Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, _
         keySize)
    End Function

    Private Function Encrypt(ByVal plainText As String, ByVal passPhrase As String, ByVal saltValue As String, ByVal hashAlgorithm As String, ByVal passwordIterations As Integer, ByVal initVector As String, _
     ByVal keySize As Integer) As String
        ' Convert strings into byte arrays.
        ' Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8 
        ' encoding.
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our plaintext into a byte array.
        ' Let us assume that plaintext contains UTF8-encoded characters.
        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)

        ' First, we must create a password, from which the key will be derived.
        ' This password will be generated from the specified passphrase and 
        ' salt value. The password will be created using the specified hash 
        ' algorithm. Password creation can be done in several iterations.
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate encryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As New MemoryStream()

        ' Define cryptographic stream (always use Write mode for encryption).
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
        ' Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

        ' Finish encrypting.
        cryptoStream.FlushFinalBlock()

        ' Convert our encrypted data from a memory stream into a byte array.
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert encrypted data into a base64-encoded string.
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)

        ' Return encrypted string.
        Return cipherText
    End Function

    ''' <summary>
    ''' Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    ''' </summary>
    ''' <param name="cipherText">
    ''' Base64-formatted ciphertext value.
    ''' </param>
    ''' <param name="passPhrase">
    ''' Passphrase from which a pseudo-random password will be derived. The
    ''' derived password will be used to generate the encryption key.
    ''' Passphrase can be any string. In this example we assume that this
    ''' passphrase is an ASCII string.
    ''' </param>
    ''' <param name="saltValue">
    ''' Salt value used along with passphrase to generate password. Salt can
    ''' be any string. In this example we assume that salt is an ASCII string.
    ''' </param>
    ''' <param name="hashAlgorithm">
    ''' Hash algorithm used to generate password. Allowed values are: "MD5" and
    ''' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    ''' </param>
    ''' <param name="passwordIterations">
    ''' Number of iterations used to generate password. One or two iterations
    ''' should be enough.
    ''' </param>
    ''' <param name="initVector">
    ''' Initialization vector (or IV). This value is required to encrypt the
    ''' first block of plaintext data. For RijndaelManaged class IV must be
    ''' exactly 16 ASCII characters long.
    ''' </param>
    ''' <param name="keySize">
    ''' Size of encryption key in bits. Allowed values are: 128, 192, and 256.
    ''' Longer keys are more secure than shorter keys.
    ''' </param>
    ''' <returns>
    ''' Decrypted string value.
    ''' </returns>
    ''' <remarks>
    ''' Most of the logic in this function is similar to the Encrypt
    ''' logic. In order for decryption to work, all parameters of this function
    ''' - except cipherText value - must match the corresponding parameters of
    ''' the Encrypt function which was called to generate the
    ''' ciphertext.
    ''' </remarks>
    Public Function DecryptString(ByVal cipherText As String) As String
        Return Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, _
         keySize)
    End Function
    Private Function Decrypt(ByVal cipherText As String, ByVal passPhrase As String, ByVal saltValue As String, ByVal hashAlgorithm As String, ByVal passwordIterations As Integer, ByVal initVector As String, _
     ByVal keySize As Integer) As String
        ' Convert strings defining encryption key characteristics into byte
        ' arrays. Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)

        ' First, we must create a password, from which the key will be 
        ' derived. This password will be generated from the specified 
        ' passphrase and salt value. The password will be created using
        ' the specified hash algorithm. Password creation can be done in
        ' several iterations.
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate decryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As New MemoryStream(cipherTextBytes)

        ' Define cryptographic stream (always use Read mode for encryption).
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

        ' Start decrypting.
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert decrypted data into a string. 
        ' Let us assume that the original plaintext string was UTF8-encoded.
        Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        ' Return decrypted string.   
        Return plainText
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

