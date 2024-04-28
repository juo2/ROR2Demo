using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200004C RID: 76
	[Flags]
	public enum KeyCombination
	{
		// Token: 0x0400019E RID: 414
		ModifierShift = 16,
		// Token: 0x0400019F RID: 415
		KeyTypeMask = 65535,
		// Token: 0x040001A0 RID: 416
		ModifierMask = -65536,
		// Token: 0x040001A1 RID: 417
		Shift = 65536,
		// Token: 0x040001A2 RID: 418
		Control = 131072,
		// Token: 0x040001A3 RID: 419
		Alt = 262144,
		// Token: 0x040001A4 RID: 420
		Meta = 524288,
		// Token: 0x040001A5 RID: 421
		ValidModifierMask = 983040,
		// Token: 0x040001A6 RID: 422
		None = 0,
		// Token: 0x040001A7 RID: 423
		Space = 1,
		// Token: 0x040001A8 RID: 424
		Backspace = 2,
		// Token: 0x040001A9 RID: 425
		Tab = 3,
		// Token: 0x040001AA RID: 426
		Escape = 4,
		// Token: 0x040001AB RID: 427
		PageUp = 5,
		// Token: 0x040001AC RID: 428
		PageDown = 6,
		// Token: 0x040001AD RID: 429
		End = 7,
		// Token: 0x040001AE RID: 430
		Home = 8,
		// Token: 0x040001AF RID: 431
		Insert = 9,
		// Token: 0x040001B0 RID: 432
		Delete = 10,
		// Token: 0x040001B1 RID: 433
		Left = 11,
		// Token: 0x040001B2 RID: 434
		Up = 12,
		// Token: 0x040001B3 RID: 435
		Right = 13,
		// Token: 0x040001B4 RID: 436
		Down = 14,
		// Token: 0x040001B5 RID: 437
		Key0 = 15,
		// Token: 0x040001B6 RID: 438
		Key1 = 16,
		// Token: 0x040001B7 RID: 439
		Key2 = 17,
		// Token: 0x040001B8 RID: 440
		Key3 = 18,
		// Token: 0x040001B9 RID: 441
		Key4 = 19,
		// Token: 0x040001BA RID: 442
		Key5 = 20,
		// Token: 0x040001BB RID: 443
		Key6 = 21,
		// Token: 0x040001BC RID: 444
		Key7 = 22,
		// Token: 0x040001BD RID: 445
		Key8 = 23,
		// Token: 0x040001BE RID: 446
		Key9 = 24,
		// Token: 0x040001BF RID: 447
		KeyA = 25,
		// Token: 0x040001C0 RID: 448
		KeyB = 26,
		// Token: 0x040001C1 RID: 449
		KeyC = 27,
		// Token: 0x040001C2 RID: 450
		KeyD = 28,
		// Token: 0x040001C3 RID: 451
		KeyE = 29,
		// Token: 0x040001C4 RID: 452
		KeyF = 30,
		// Token: 0x040001C5 RID: 453
		KeyG = 31,
		// Token: 0x040001C6 RID: 454
		KeyH = 32,
		// Token: 0x040001C7 RID: 455
		KeyI = 33,
		// Token: 0x040001C8 RID: 456
		KeyJ = 34,
		// Token: 0x040001C9 RID: 457
		KeyK = 35,
		// Token: 0x040001CA RID: 458
		KeyL = 36,
		// Token: 0x040001CB RID: 459
		KeyM = 37,
		// Token: 0x040001CC RID: 460
		KeyN = 38,
		// Token: 0x040001CD RID: 461
		KeyO = 39,
		// Token: 0x040001CE RID: 462
		KeyP = 40,
		// Token: 0x040001CF RID: 463
		KeyQ = 41,
		// Token: 0x040001D0 RID: 464
		KeyR = 42,
		// Token: 0x040001D1 RID: 465
		KeyS = 43,
		// Token: 0x040001D2 RID: 466
		KeyT = 44,
		// Token: 0x040001D3 RID: 467
		KeyU = 45,
		// Token: 0x040001D4 RID: 468
		KeyV = 46,
		// Token: 0x040001D5 RID: 469
		KeyW = 47,
		// Token: 0x040001D6 RID: 470
		KeyX = 48,
		// Token: 0x040001D7 RID: 471
		KeyY = 49,
		// Token: 0x040001D8 RID: 472
		KeyZ = 50,
		// Token: 0x040001D9 RID: 473
		Numpad0 = 51,
		// Token: 0x040001DA RID: 474
		Numpad1 = 52,
		// Token: 0x040001DB RID: 475
		Numpad2 = 53,
		// Token: 0x040001DC RID: 476
		Numpad3 = 54,
		// Token: 0x040001DD RID: 477
		Numpad4 = 55,
		// Token: 0x040001DE RID: 478
		Numpad5 = 56,
		// Token: 0x040001DF RID: 479
		Numpad6 = 57,
		// Token: 0x040001E0 RID: 480
		Numpad7 = 58,
		// Token: 0x040001E1 RID: 481
		Numpad8 = 59,
		// Token: 0x040001E2 RID: 482
		Numpad9 = 60,
		// Token: 0x040001E3 RID: 483
		NumpadAsterisk = 61,
		// Token: 0x040001E4 RID: 484
		NumpadPlus = 62,
		// Token: 0x040001E5 RID: 485
		NumpadMinus = 63,
		// Token: 0x040001E6 RID: 486
		NumpadPeriod = 64,
		// Token: 0x040001E7 RID: 487
		NumpadDivide = 65,
		// Token: 0x040001E8 RID: 488
		F1 = 66,
		// Token: 0x040001E9 RID: 489
		F2 = 67,
		// Token: 0x040001EA RID: 490
		F3 = 68,
		// Token: 0x040001EB RID: 491
		F4 = 69,
		// Token: 0x040001EC RID: 492
		F5 = 70,
		// Token: 0x040001ED RID: 493
		F6 = 71,
		// Token: 0x040001EE RID: 494
		F7 = 72,
		// Token: 0x040001EF RID: 495
		F8 = 73,
		// Token: 0x040001F0 RID: 496
		F9 = 74,
		// Token: 0x040001F1 RID: 497
		F10 = 75,
		// Token: 0x040001F2 RID: 498
		F11 = 76,
		// Token: 0x040001F3 RID: 499
		F12 = 77,
		// Token: 0x040001F4 RID: 500
		F13 = 78,
		// Token: 0x040001F5 RID: 501
		F14 = 79,
		// Token: 0x040001F6 RID: 502
		F15 = 80,
		// Token: 0x040001F7 RID: 503
		F16 = 81,
		// Token: 0x040001F8 RID: 504
		F17 = 82,
		// Token: 0x040001F9 RID: 505
		F18 = 83,
		// Token: 0x040001FA RID: 506
		F19 = 84,
		// Token: 0x040001FB RID: 507
		F20 = 85,
		// Token: 0x040001FC RID: 508
		F21 = 86,
		// Token: 0x040001FD RID: 509
		F22 = 87,
		// Token: 0x040001FE RID: 510
		F23 = 88,
		// Token: 0x040001FF RID: 511
		F24 = 89,
		// Token: 0x04000200 RID: 512
		OemPlus = 90,
		// Token: 0x04000201 RID: 513
		OemComma = 91,
		// Token: 0x04000202 RID: 514
		OemMinus = 92,
		// Token: 0x04000203 RID: 515
		OemPeriod = 93,
		// Token: 0x04000204 RID: 516
		Oem1 = 94,
		// Token: 0x04000205 RID: 517
		Oem2 = 95,
		// Token: 0x04000206 RID: 518
		Oem3 = 96,
		// Token: 0x04000207 RID: 519
		Oem4 = 97,
		// Token: 0x04000208 RID: 520
		Oem5 = 98,
		// Token: 0x04000209 RID: 521
		Oem6 = 99,
		// Token: 0x0400020A RID: 522
		Oem7 = 100,
		// Token: 0x0400020B RID: 523
		Oem8 = 101,
		// Token: 0x0400020C RID: 524
		MaxKeyType = 102
	}
}
