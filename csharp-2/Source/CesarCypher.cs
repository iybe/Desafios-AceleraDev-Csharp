using System;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {
        static int avance(int num, int qtd)
        {
            num = num + qtd;
            if (num >= 'a' && num <= 'z')
            {
                return num;
            }
            if (num > 'z')
            {
                num = 'a' + (num - 'z' - 1);
            }
            if (num < 'a')
            {
                num = 'z' - ('a' - num - 1);
            }
            return num;
        }

        public string Crypt(string message)
        {
            if(message == null)
            {
                throw new ArgumentNullException();
            }

            message = message.ToLower();
            string resp = "";
            for (int i = 0; i < message.Length; i++)
            {
                char c = message[i];
                if(c >= 'a' && c <= 'z')
                {
                    c = (char)avance((int)c,3);
                }
                else if(c != ' ' && !(c >= '0' && c <= '9'))
                {
                    throw new ArgumentOutOfRangeException();
                }
                resp += c;
            }
            return resp;
        }

        public string Decrypt(string cryptedMessage)
        {
            if (cryptedMessage == null)
            {
                throw new ArgumentNullException();
            }
            cryptedMessage = cryptedMessage.ToLower();
            string resp = "";
            for (int i = 0; i < cryptedMessage.Length; i++)
            {
                char c = cryptedMessage[i];
                if (c >= 'a' && c <= 'z')
                {
                    c = (char)avance((int)c, -3);
                }
                else if (c != ' ' && !(c >= '0' && c <= '9'))
                {
                    throw new ArgumentOutOfRangeException();
                }
                resp += c;
            }
            return resp;
        }
    }
}
