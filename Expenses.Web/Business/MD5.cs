using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business
{
  public sealed class MD5
  {
    public static async Task<string> HashAsync(string input, CancellationToken cancellationToken = default)
    {
      using (var crypto = System.Security.Cryptography.MD5.Create())
      using (var stream = new MemoryStream())
      {
        var inputBytes = Encoding.ASCII.GetBytes(input);

        await stream.WriteAsync(inputBytes, cancellationToken);

        var hashBytes = await crypto.ComputeHashAsync(stream, cancellationToken);

        var sb = new StringBuilder();
        foreach (var b in hashBytes)
        {
          sb.Append($"{b:x2}");
        }

        return sb.ToString();
      }
    }
  }
}
