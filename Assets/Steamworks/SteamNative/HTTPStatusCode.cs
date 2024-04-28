using System;

namespace SteamNative
{
	// Token: 0x02000042 RID: 66
	internal enum HTTPStatusCode
	{
		// Token: 0x0400029C RID: 668
		Invalid,
		// Token: 0x0400029D RID: 669
		HTTPStatusCode100Continue = 100,
		// Token: 0x0400029E RID: 670
		HTTPStatusCode101SwitchingProtocols,
		// Token: 0x0400029F RID: 671
		HTTPStatusCode200OK = 200,
		// Token: 0x040002A0 RID: 672
		HTTPStatusCode201Created,
		// Token: 0x040002A1 RID: 673
		HTTPStatusCode202Accepted,
		// Token: 0x040002A2 RID: 674
		HTTPStatusCode203NonAuthoritative,
		// Token: 0x040002A3 RID: 675
		HTTPStatusCode204NoContent,
		// Token: 0x040002A4 RID: 676
		HTTPStatusCode205ResetContent,
		// Token: 0x040002A5 RID: 677
		HTTPStatusCode206PartialContent,
		// Token: 0x040002A6 RID: 678
		HTTPStatusCode300MultipleChoices = 300,
		// Token: 0x040002A7 RID: 679
		HTTPStatusCode301MovedPermanently,
		// Token: 0x040002A8 RID: 680
		HTTPStatusCode302Found,
		// Token: 0x040002A9 RID: 681
		HTTPStatusCode303SeeOther,
		// Token: 0x040002AA RID: 682
		HTTPStatusCode304NotModified,
		// Token: 0x040002AB RID: 683
		HTTPStatusCode305UseProxy,
		// Token: 0x040002AC RID: 684
		HTTPStatusCode307TemporaryRedirect = 307,
		// Token: 0x040002AD RID: 685
		HTTPStatusCode400BadRequest = 400,
		// Token: 0x040002AE RID: 686
		HTTPStatusCode401Unauthorized,
		// Token: 0x040002AF RID: 687
		HTTPStatusCode402PaymentRequired,
		// Token: 0x040002B0 RID: 688
		HTTPStatusCode403Forbidden,
		// Token: 0x040002B1 RID: 689
		HTTPStatusCode404NotFound,
		// Token: 0x040002B2 RID: 690
		HTTPStatusCode405MethodNotAllowed,
		// Token: 0x040002B3 RID: 691
		HTTPStatusCode406NotAcceptable,
		// Token: 0x040002B4 RID: 692
		HTTPStatusCode407ProxyAuthRequired,
		// Token: 0x040002B5 RID: 693
		HTTPStatusCode408RequestTimeout,
		// Token: 0x040002B6 RID: 694
		HTTPStatusCode409Conflict,
		// Token: 0x040002B7 RID: 695
		HTTPStatusCode410Gone,
		// Token: 0x040002B8 RID: 696
		HTTPStatusCode411LengthRequired,
		// Token: 0x040002B9 RID: 697
		HTTPStatusCode412PreconditionFailed,
		// Token: 0x040002BA RID: 698
		HTTPStatusCode413RequestEntityTooLarge,
		// Token: 0x040002BB RID: 699
		HTTPStatusCode414RequestURITooLong,
		// Token: 0x040002BC RID: 700
		HTTPStatusCode415UnsupportedMediaType,
		// Token: 0x040002BD RID: 701
		HTTPStatusCode416RequestedRangeNotSatisfiable,
		// Token: 0x040002BE RID: 702
		HTTPStatusCode417ExpectationFailed,
		// Token: 0x040002BF RID: 703
		HTTPStatusCode4xxUnknown,
		// Token: 0x040002C0 RID: 704
		HTTPStatusCode429TooManyRequests = 429,
		// Token: 0x040002C1 RID: 705
		HTTPStatusCode500InternalServerError = 500,
		// Token: 0x040002C2 RID: 706
		HTTPStatusCode501NotImplemented,
		// Token: 0x040002C3 RID: 707
		HTTPStatusCode502BadGateway,
		// Token: 0x040002C4 RID: 708
		HTTPStatusCode503ServiceUnavailable,
		// Token: 0x040002C5 RID: 709
		HTTPStatusCode504GatewayTimeout,
		// Token: 0x040002C6 RID: 710
		HTTPStatusCode505HTTPVersionNotSupported,
		// Token: 0x040002C7 RID: 711
		HTTPStatusCode5xxUnknown = 599
	}
}
