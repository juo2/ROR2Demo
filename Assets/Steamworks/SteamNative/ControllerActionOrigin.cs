using System;

namespace SteamNative
{
	// Token: 0x02000046 RID: 70
	internal enum ControllerActionOrigin
	{
		// Token: 0x040002EE RID: 750
		None,
		// Token: 0x040002EF RID: 751
		A,
		// Token: 0x040002F0 RID: 752
		B,
		// Token: 0x040002F1 RID: 753
		X,
		// Token: 0x040002F2 RID: 754
		Y,
		// Token: 0x040002F3 RID: 755
		LeftBumper,
		// Token: 0x040002F4 RID: 756
		RightBumper,
		// Token: 0x040002F5 RID: 757
		LeftGrip,
		// Token: 0x040002F6 RID: 758
		RightGrip,
		// Token: 0x040002F7 RID: 759
		Start,
		// Token: 0x040002F8 RID: 760
		Back,
		// Token: 0x040002F9 RID: 761
		LeftPad_Touch,
		// Token: 0x040002FA RID: 762
		LeftPad_Swipe,
		// Token: 0x040002FB RID: 763
		LeftPad_Click,
		// Token: 0x040002FC RID: 764
		LeftPad_DPadNorth,
		// Token: 0x040002FD RID: 765
		LeftPad_DPadSouth,
		// Token: 0x040002FE RID: 766
		LeftPad_DPadWest,
		// Token: 0x040002FF RID: 767
		LeftPad_DPadEast,
		// Token: 0x04000300 RID: 768
		RightPad_Touch,
		// Token: 0x04000301 RID: 769
		RightPad_Swipe,
		// Token: 0x04000302 RID: 770
		RightPad_Click,
		// Token: 0x04000303 RID: 771
		RightPad_DPadNorth,
		// Token: 0x04000304 RID: 772
		RightPad_DPadSouth,
		// Token: 0x04000305 RID: 773
		RightPad_DPadWest,
		// Token: 0x04000306 RID: 774
		RightPad_DPadEast,
		// Token: 0x04000307 RID: 775
		LeftTrigger_Pull,
		// Token: 0x04000308 RID: 776
		LeftTrigger_Click,
		// Token: 0x04000309 RID: 777
		RightTrigger_Pull,
		// Token: 0x0400030A RID: 778
		RightTrigger_Click,
		// Token: 0x0400030B RID: 779
		LeftStick_Move,
		// Token: 0x0400030C RID: 780
		LeftStick_Click,
		// Token: 0x0400030D RID: 781
		LeftStick_DPadNorth,
		// Token: 0x0400030E RID: 782
		LeftStick_DPadSouth,
		// Token: 0x0400030F RID: 783
		LeftStick_DPadWest,
		// Token: 0x04000310 RID: 784
		LeftStick_DPadEast,
		// Token: 0x04000311 RID: 785
		Gyro_Move,
		// Token: 0x04000312 RID: 786
		Gyro_Pitch,
		// Token: 0x04000313 RID: 787
		Gyro_Yaw,
		// Token: 0x04000314 RID: 788
		Gyro_Roll,
		// Token: 0x04000315 RID: 789
		PS4_X,
		// Token: 0x04000316 RID: 790
		PS4_Circle,
		// Token: 0x04000317 RID: 791
		PS4_Triangle,
		// Token: 0x04000318 RID: 792
		PS4_Square,
		// Token: 0x04000319 RID: 793
		PS4_LeftBumper,
		// Token: 0x0400031A RID: 794
		PS4_RightBumper,
		// Token: 0x0400031B RID: 795
		PS4_Options,
		// Token: 0x0400031C RID: 796
		PS4_Share,
		// Token: 0x0400031D RID: 797
		PS4_LeftPad_Touch,
		// Token: 0x0400031E RID: 798
		PS4_LeftPad_Swipe,
		// Token: 0x0400031F RID: 799
		PS4_LeftPad_Click,
		// Token: 0x04000320 RID: 800
		PS4_LeftPad_DPadNorth,
		// Token: 0x04000321 RID: 801
		PS4_LeftPad_DPadSouth,
		// Token: 0x04000322 RID: 802
		PS4_LeftPad_DPadWest,
		// Token: 0x04000323 RID: 803
		PS4_LeftPad_DPadEast,
		// Token: 0x04000324 RID: 804
		PS4_RightPad_Touch,
		// Token: 0x04000325 RID: 805
		PS4_RightPad_Swipe,
		// Token: 0x04000326 RID: 806
		PS4_RightPad_Click,
		// Token: 0x04000327 RID: 807
		PS4_RightPad_DPadNorth,
		// Token: 0x04000328 RID: 808
		PS4_RightPad_DPadSouth,
		// Token: 0x04000329 RID: 809
		PS4_RightPad_DPadWest,
		// Token: 0x0400032A RID: 810
		PS4_RightPad_DPadEast,
		// Token: 0x0400032B RID: 811
		PS4_CenterPad_Touch,
		// Token: 0x0400032C RID: 812
		PS4_CenterPad_Swipe,
		// Token: 0x0400032D RID: 813
		PS4_CenterPad_Click,
		// Token: 0x0400032E RID: 814
		PS4_CenterPad_DPadNorth,
		// Token: 0x0400032F RID: 815
		PS4_CenterPad_DPadSouth,
		// Token: 0x04000330 RID: 816
		PS4_CenterPad_DPadWest,
		// Token: 0x04000331 RID: 817
		PS4_CenterPad_DPadEast,
		// Token: 0x04000332 RID: 818
		PS4_LeftTrigger_Pull,
		// Token: 0x04000333 RID: 819
		PS4_LeftTrigger_Click,
		// Token: 0x04000334 RID: 820
		PS4_RightTrigger_Pull,
		// Token: 0x04000335 RID: 821
		PS4_RightTrigger_Click,
		// Token: 0x04000336 RID: 822
		PS4_LeftStick_Move,
		// Token: 0x04000337 RID: 823
		PS4_LeftStick_Click,
		// Token: 0x04000338 RID: 824
		PS4_LeftStick_DPadNorth,
		// Token: 0x04000339 RID: 825
		PS4_LeftStick_DPadSouth,
		// Token: 0x0400033A RID: 826
		PS4_LeftStick_DPadWest,
		// Token: 0x0400033B RID: 827
		PS4_LeftStick_DPadEast,
		// Token: 0x0400033C RID: 828
		PS4_RightStick_Move,
		// Token: 0x0400033D RID: 829
		PS4_RightStick_Click,
		// Token: 0x0400033E RID: 830
		PS4_RightStick_DPadNorth,
		// Token: 0x0400033F RID: 831
		PS4_RightStick_DPadSouth,
		// Token: 0x04000340 RID: 832
		PS4_RightStick_DPadWest,
		// Token: 0x04000341 RID: 833
		PS4_RightStick_DPadEast,
		// Token: 0x04000342 RID: 834
		PS4_DPad_North,
		// Token: 0x04000343 RID: 835
		PS4_DPad_South,
		// Token: 0x04000344 RID: 836
		PS4_DPad_West,
		// Token: 0x04000345 RID: 837
		PS4_DPad_East,
		// Token: 0x04000346 RID: 838
		PS4_Gyro_Move,
		// Token: 0x04000347 RID: 839
		PS4_Gyro_Pitch,
		// Token: 0x04000348 RID: 840
		PS4_Gyro_Yaw,
		// Token: 0x04000349 RID: 841
		PS4_Gyro_Roll,
		// Token: 0x0400034A RID: 842
		XBoxOne_A,
		// Token: 0x0400034B RID: 843
		XBoxOne_B,
		// Token: 0x0400034C RID: 844
		XBoxOne_X,
		// Token: 0x0400034D RID: 845
		XBoxOne_Y,
		// Token: 0x0400034E RID: 846
		XBoxOne_LeftBumper,
		// Token: 0x0400034F RID: 847
		XBoxOne_RightBumper,
		// Token: 0x04000350 RID: 848
		XBoxOne_Menu,
		// Token: 0x04000351 RID: 849
		XBoxOne_View,
		// Token: 0x04000352 RID: 850
		XBoxOne_LeftTrigger_Pull,
		// Token: 0x04000353 RID: 851
		XBoxOne_LeftTrigger_Click,
		// Token: 0x04000354 RID: 852
		XBoxOne_RightTrigger_Pull,
		// Token: 0x04000355 RID: 853
		XBoxOne_RightTrigger_Click,
		// Token: 0x04000356 RID: 854
		XBoxOne_LeftStick_Move,
		// Token: 0x04000357 RID: 855
		XBoxOne_LeftStick_Click,
		// Token: 0x04000358 RID: 856
		XBoxOne_LeftStick_DPadNorth,
		// Token: 0x04000359 RID: 857
		XBoxOne_LeftStick_DPadSouth,
		// Token: 0x0400035A RID: 858
		XBoxOne_LeftStick_DPadWest,
		// Token: 0x0400035B RID: 859
		XBoxOne_LeftStick_DPadEast,
		// Token: 0x0400035C RID: 860
		XBoxOne_RightStick_Move,
		// Token: 0x0400035D RID: 861
		XBoxOne_RightStick_Click,
		// Token: 0x0400035E RID: 862
		XBoxOne_RightStick_DPadNorth,
		// Token: 0x0400035F RID: 863
		XBoxOne_RightStick_DPadSouth,
		// Token: 0x04000360 RID: 864
		XBoxOne_RightStick_DPadWest,
		// Token: 0x04000361 RID: 865
		XBoxOne_RightStick_DPadEast,
		// Token: 0x04000362 RID: 866
		XBoxOne_DPad_North,
		// Token: 0x04000363 RID: 867
		XBoxOne_DPad_South,
		// Token: 0x04000364 RID: 868
		XBoxOne_DPad_West,
		// Token: 0x04000365 RID: 869
		XBoxOne_DPad_East,
		// Token: 0x04000366 RID: 870
		XBox360_A,
		// Token: 0x04000367 RID: 871
		XBox360_B,
		// Token: 0x04000368 RID: 872
		XBox360_X,
		// Token: 0x04000369 RID: 873
		XBox360_Y,
		// Token: 0x0400036A RID: 874
		XBox360_LeftBumper,
		// Token: 0x0400036B RID: 875
		XBox360_RightBumper,
		// Token: 0x0400036C RID: 876
		XBox360_Start,
		// Token: 0x0400036D RID: 877
		XBox360_Back,
		// Token: 0x0400036E RID: 878
		XBox360_LeftTrigger_Pull,
		// Token: 0x0400036F RID: 879
		XBox360_LeftTrigger_Click,
		// Token: 0x04000370 RID: 880
		XBox360_RightTrigger_Pull,
		// Token: 0x04000371 RID: 881
		XBox360_RightTrigger_Click,
		// Token: 0x04000372 RID: 882
		XBox360_LeftStick_Move,
		// Token: 0x04000373 RID: 883
		XBox360_LeftStick_Click,
		// Token: 0x04000374 RID: 884
		XBox360_LeftStick_DPadNorth,
		// Token: 0x04000375 RID: 885
		XBox360_LeftStick_DPadSouth,
		// Token: 0x04000376 RID: 886
		XBox360_LeftStick_DPadWest,
		// Token: 0x04000377 RID: 887
		XBox360_LeftStick_DPadEast,
		// Token: 0x04000378 RID: 888
		XBox360_RightStick_Move,
		// Token: 0x04000379 RID: 889
		XBox360_RightStick_Click,
		// Token: 0x0400037A RID: 890
		XBox360_RightStick_DPadNorth,
		// Token: 0x0400037B RID: 891
		XBox360_RightStick_DPadSouth,
		// Token: 0x0400037C RID: 892
		XBox360_RightStick_DPadWest,
		// Token: 0x0400037D RID: 893
		XBox360_RightStick_DPadEast,
		// Token: 0x0400037E RID: 894
		XBox360_DPad_North,
		// Token: 0x0400037F RID: 895
		XBox360_DPad_South,
		// Token: 0x04000380 RID: 896
		XBox360_DPad_West,
		// Token: 0x04000381 RID: 897
		XBox360_DPad_East,
		// Token: 0x04000382 RID: 898
		SteamV2_A,
		// Token: 0x04000383 RID: 899
		SteamV2_B,
		// Token: 0x04000384 RID: 900
		SteamV2_X,
		// Token: 0x04000385 RID: 901
		SteamV2_Y,
		// Token: 0x04000386 RID: 902
		SteamV2_LeftBumper,
		// Token: 0x04000387 RID: 903
		SteamV2_RightBumper,
		// Token: 0x04000388 RID: 904
		SteamV2_LeftGrip,
		// Token: 0x04000389 RID: 905
		SteamV2_RightGrip,
		// Token: 0x0400038A RID: 906
		SteamV2_LeftGrip_Upper,
		// Token: 0x0400038B RID: 907
		SteamV2_RightGrip_Upper,
		// Token: 0x0400038C RID: 908
		SteamV2_LeftBumper_Pressure,
		// Token: 0x0400038D RID: 909
		SteamV2_RightBumper_Pressure,
		// Token: 0x0400038E RID: 910
		SteamV2_LeftGrip_Pressure,
		// Token: 0x0400038F RID: 911
		SteamV2_RightGrip_Pressure,
		// Token: 0x04000390 RID: 912
		SteamV2_LeftGrip_Upper_Pressure,
		// Token: 0x04000391 RID: 913
		SteamV2_RightGrip_Upper_Pressure,
		// Token: 0x04000392 RID: 914
		SteamV2_Start,
		// Token: 0x04000393 RID: 915
		SteamV2_Back,
		// Token: 0x04000394 RID: 916
		SteamV2_LeftPad_Touch,
		// Token: 0x04000395 RID: 917
		SteamV2_LeftPad_Swipe,
		// Token: 0x04000396 RID: 918
		SteamV2_LeftPad_Click,
		// Token: 0x04000397 RID: 919
		SteamV2_LeftPad_Pressure,
		// Token: 0x04000398 RID: 920
		SteamV2_LeftPad_DPadNorth,
		// Token: 0x04000399 RID: 921
		SteamV2_LeftPad_DPadSouth,
		// Token: 0x0400039A RID: 922
		SteamV2_LeftPad_DPadWest,
		// Token: 0x0400039B RID: 923
		SteamV2_LeftPad_DPadEast,
		// Token: 0x0400039C RID: 924
		SteamV2_RightPad_Touch,
		// Token: 0x0400039D RID: 925
		SteamV2_RightPad_Swipe,
		// Token: 0x0400039E RID: 926
		SteamV2_RightPad_Click,
		// Token: 0x0400039F RID: 927
		SteamV2_RightPad_Pressure,
		// Token: 0x040003A0 RID: 928
		SteamV2_RightPad_DPadNorth,
		// Token: 0x040003A1 RID: 929
		SteamV2_RightPad_DPadSouth,
		// Token: 0x040003A2 RID: 930
		SteamV2_RightPad_DPadWest,
		// Token: 0x040003A3 RID: 931
		SteamV2_RightPad_DPadEast,
		// Token: 0x040003A4 RID: 932
		SteamV2_LeftTrigger_Pull,
		// Token: 0x040003A5 RID: 933
		SteamV2_LeftTrigger_Click,
		// Token: 0x040003A6 RID: 934
		SteamV2_RightTrigger_Pull,
		// Token: 0x040003A7 RID: 935
		SteamV2_RightTrigger_Click,
		// Token: 0x040003A8 RID: 936
		SteamV2_LeftStick_Move,
		// Token: 0x040003A9 RID: 937
		SteamV2_LeftStick_Click,
		// Token: 0x040003AA RID: 938
		SteamV2_LeftStick_DPadNorth,
		// Token: 0x040003AB RID: 939
		SteamV2_LeftStick_DPadSouth,
		// Token: 0x040003AC RID: 940
		SteamV2_LeftStick_DPadWest,
		// Token: 0x040003AD RID: 941
		SteamV2_LeftStick_DPadEast,
		// Token: 0x040003AE RID: 942
		SteamV2_Gyro_Move,
		// Token: 0x040003AF RID: 943
		SteamV2_Gyro_Pitch,
		// Token: 0x040003B0 RID: 944
		SteamV2_Gyro_Yaw,
		// Token: 0x040003B1 RID: 945
		SteamV2_Gyro_Roll,
		// Token: 0x040003B2 RID: 946
		Count
	}
}
