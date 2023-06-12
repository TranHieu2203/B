Public Module mdlConvertFont

    Private UniByte(135) As Long
    Private UniMap(135) As String

    Private VNIByte1(135) As Long
    Private VNIByte2(135) As Long
    Private VNIMap(135) As String

    ' Font PCW Time cung hoi giong VNI-TIME
    Private PCWByte1(135) As Long
    Private PCWByte2(135) As Long
    Private PCWMap(135) As String

    Private ABCByte(135) As Long
    Private ABCMap(135) As String

    Private VietWareFByte(135) As Long
    Private VietWareFMap(135) As String

    Private VIWByte1(135) As Long
    Private VIWByte2(135) As Long
    Private VIWMap(135) As String

    Private KhongDauMap(135) As String

    Public Enum FontType
        xUnicode = 0
        VNI = 1
        abc = 2
        VietWare = 3
        PCWTimes = 4
        KhongDau = 5
        VietWareF = 6
    End Enum

    Private Sub Init()
        Dim lngCtr As Long
        If UniMap(135) = "a" Then Exit Sub
        UniMap(135) = "a"

        ' Tao bang ma cua Unicode
        UniByte(0) = 225
        UniByte(1) = 224
        UniByte(2) = 7843
        UniByte(3) = 227
        UniByte(4) = 7841
        UniByte(5) = 226
        UniByte(6) = 7845
        UniByte(7) = 7847
        UniByte(8) = 7849
        UniByte(9) = 7851
        UniByte(10) = 7853
        UniByte(11) = 259
        UniByte(12) = 7855
        UniByte(13) = 7857
        UniByte(14) = 7859
        UniByte(15) = 7861
        UniByte(16) = 7863
        UniByte(17) = 233
        UniByte(18) = 232
        UniByte(19) = 7867
        UniByte(20) = 7869
        UniByte(21) = 7865
        UniByte(22) = 234
        UniByte(23) = 7871
        UniByte(24) = 7873
        UniByte(25) = 7875
        UniByte(26) = 7877
        UniByte(27) = 7879
        UniByte(28) = 237
        UniByte(29) = 236
        UniByte(30) = 7881
        UniByte(31) = 297
        UniByte(32) = 7883
        UniByte(33) = 243
        UniByte(34) = 242
        UniByte(35) = 7887
        UniByte(36) = 245
        UniByte(37) = 7885
        UniByte(38) = 244
        UniByte(39) = 7889
        UniByte(40) = 7891
        UniByte(41) = 7893
        UniByte(42) = 7895
        UniByte(43) = 7897
        UniByte(44) = 417
        UniByte(45) = 7899
        UniByte(46) = 7901
        UniByte(47) = 7903
        UniByte(48) = 7905
        UniByte(49) = 7907
        UniByte(50) = 250
        UniByte(51) = 249
        UniByte(52) = 7911
        UniByte(53) = 361
        UniByte(54) = 7909
        UniByte(55) = 432
        UniByte(56) = 7913
        UniByte(57) = 7915
        UniByte(58) = 7917
        UniByte(59) = 7919
        UniByte(60) = 7921
        UniByte(61) = 253
        UniByte(62) = 7923
        UniByte(63) = 7927
        UniByte(64) = 7929
        UniByte(65) = 7925
        UniByte(66) = 273
        UniByte(67) = 193
        UniByte(68) = 192
        UniByte(69) = 7842
        UniByte(70) = 195
        UniByte(71) = 7840
        UniByte(72) = 194
        UniByte(73) = 7844
        UniByte(74) = 7846
        UniByte(75) = 7848
        UniByte(76) = 7850
        UniByte(77) = 7852
        UniByte(78) = 258
        UniByte(79) = 7854
        UniByte(80) = 7856
        UniByte(81) = 7858
        UniByte(82) = 7860
        UniByte(83) = 7862
        UniByte(84) = 201
        UniByte(85) = 200
        UniByte(86) = 7866
        UniByte(87) = 7868
        UniByte(88) = 7864
        UniByte(89) = 202
        UniByte(90) = 7870
        UniByte(91) = 7872
        UniByte(92) = 7874
        UniByte(93) = 7876
        UniByte(94) = 7878
        UniByte(95) = 205
        UniByte(96) = 204
        UniByte(97) = 7880
        UniByte(98) = 296
        UniByte(99) = 7882
        UniByte(100) = 211
        UniByte(101) = 210
        UniByte(102) = 7886
        UniByte(103) = 213
        UniByte(104) = 7884
        UniByte(105) = 212
        UniByte(106) = 7888
        UniByte(107) = 7890
        UniByte(108) = 7892
        UniByte(109) = 7894
        UniByte(110) = 7896
        UniByte(111) = 416
        UniByte(112) = 7898
        UniByte(113) = 7900
        UniByte(114) = 7902
        UniByte(115) = 7904
        UniByte(116) = 7906
        UniByte(117) = 218
        UniByte(118) = 217
        UniByte(119) = 7910
        UniByte(120) = 360
        UniByte(121) = 7908
        UniByte(122) = 431
        UniByte(123) = 7912
        UniByte(124) = 7914
        UniByte(125) = 7916
        UniByte(126) = 7918
        UniByte(127) = 7920
        UniByte(128) = 221
        UniByte(129) = 7922
        UniByte(130) = 7926
        UniByte(131) = 7928
        UniByte(132) = 7924
        UniByte(133) = 272 '208

        For lngCtr = 0 To 133
            UniMap(lngCtr) = ChrW(UniByte(lngCtr))
        Next

        'Tao bang cua VNI
        VNIByte1(0) = 97
        VNIByte2(0) = 249
        VNIByte1(1) = 97
        VNIByte2(1) = 248
        VNIByte1(2) = 97
        VNIByte2(2) = 251
        VNIByte1(3) = 97
        VNIByte2(3) = 245
        VNIByte1(4) = 97
        VNIByte2(4) = 239
        VNIByte1(5) = 97
        VNIByte2(5) = 226
        VNIByte1(6) = 97
        VNIByte2(6) = 225
        VNIByte1(7) = 97
        VNIByte2(7) = 224
        VNIByte1(8) = 97
        VNIByte2(8) = 229
        VNIByte1(9) = 97
        VNIByte2(9) = 227
        VNIByte1(10) = 97
        VNIByte2(10) = 228
        VNIByte1(11) = 97
        VNIByte2(11) = 234
        VNIByte1(12) = 97
        VNIByte2(12) = 233
        VNIByte1(13) = 97
        VNIByte2(13) = 232
        VNIByte1(14) = 97
        VNIByte2(14) = 250
        VNIByte1(15) = 97
        VNIByte2(15) = 252
        VNIByte1(16) = 97
        VNIByte2(16) = 235
        VNIByte1(17) = 101
        VNIByte2(17) = 249
        VNIByte1(18) = 101
        VNIByte2(18) = 248
        VNIByte1(19) = 101
        VNIByte2(19) = 251
        VNIByte1(20) = 101
        VNIByte2(20) = 245
        VNIByte1(21) = 101
        VNIByte2(21) = 239
        VNIByte1(22) = 101
        VNIByte2(22) = 226
        VNIByte1(23) = 101
        VNIByte2(23) = 225
        VNIByte1(24) = 101
        VNIByte2(24) = 224
        VNIByte1(25) = 101
        VNIByte2(25) = 229
        VNIByte1(26) = 101
        VNIByte2(26) = 227
        VNIByte1(27) = 101
        VNIByte2(27) = 228
        VNIByte1(28) = 237
        VNIByte2(28) = 0
        VNIByte1(29) = 236
        VNIByte2(29) = 0
        VNIByte1(30) = 230
        VNIByte2(30) = 0
        VNIByte1(31) = 243
        VNIByte2(31) = 0
        VNIByte1(32) = 242
        VNIByte2(32) = 0
        VNIByte1(33) = 111
        VNIByte2(33) = 249
        VNIByte1(34) = 111
        VNIByte2(34) = 248
        VNIByte1(35) = 111
        VNIByte2(35) = 251
        VNIByte1(36) = 111
        VNIByte2(36) = 245
        VNIByte1(37) = 111
        VNIByte2(37) = 239
        VNIByte1(38) = 111
        VNIByte2(38) = 226
        VNIByte1(39) = 111
        VNIByte2(39) = 225
        VNIByte1(40) = 111
        VNIByte2(40) = 224
        VNIByte1(41) = 111
        VNIByte2(41) = 229
        VNIByte1(42) = 111
        VNIByte2(42) = 227
        VNIByte1(43) = 111
        VNIByte2(43) = 228
        VNIByte1(44) = 244
        VNIByte2(44) = 0
        VNIByte1(45) = 244
        VNIByte2(45) = 249
        VNIByte1(46) = 244
        VNIByte2(46) = 248
        VNIByte1(47) = 244
        VNIByte2(47) = 251
        VNIByte1(48) = 244
        VNIByte2(48) = 245
        VNIByte1(49) = 244
        VNIByte2(49) = 239
        VNIByte1(50) = 117
        VNIByte2(50) = 249
        VNIByte1(51) = 117
        VNIByte2(51) = 248
        VNIByte1(52) = 117
        VNIByte2(52) = 251
        VNIByte1(53) = 117
        VNIByte2(53) = 245
        VNIByte1(54) = 117
        VNIByte2(54) = 239
        VNIByte1(55) = 246
        VNIByte2(55) = 0
        VNIByte1(56) = 246
        VNIByte2(56) = 249
        VNIByte1(57) = 246
        VNIByte2(57) = 248
        VNIByte1(58) = 246
        VNIByte2(58) = 251
        VNIByte1(59) = 246
        VNIByte2(59) = 245
        VNIByte1(60) = 246
        VNIByte2(60) = 239
        VNIByte1(61) = 121
        VNIByte2(61) = 249
        VNIByte1(62) = 121
        VNIByte2(62) = 248
        VNIByte1(63) = 121
        VNIByte2(63) = 251
        VNIByte1(64) = 121
        VNIByte2(64) = 245
        VNIByte1(65) = 238
        VNIByte2(65) = 0
        VNIByte1(66) = 241
        VNIByte2(66) = 0
        VNIByte1(67) = 65
        VNIByte2(67) = 217
        VNIByte1(68) = 65
        VNIByte2(68) = 216
        VNIByte1(69) = 65
        VNIByte2(69) = 219
        VNIByte1(70) = 65
        VNIByte2(70) = 213
        VNIByte1(71) = 65
        VNIByte2(71) = 207
        VNIByte1(72) = 65
        VNIByte2(72) = 194
        VNIByte1(73) = 65
        VNIByte2(73) = 193
        VNIByte1(74) = 65
        VNIByte2(74) = 192
        VNIByte1(75) = 65
        VNIByte2(75) = 197
        VNIByte1(76) = 65
        VNIByte2(76) = 195
        VNIByte1(77) = 65
        VNIByte2(77) = 196
        VNIByte1(78) = 65
        VNIByte2(78) = 202
        VNIByte1(79) = 65
        VNIByte2(79) = 201
        VNIByte1(80) = 65
        VNIByte2(80) = 200
        VNIByte1(81) = 65
        VNIByte2(81) = 218
        VNIByte1(82) = 65
        VNIByte2(82) = 220
        VNIByte1(83) = 65
        VNIByte2(83) = 203
        VNIByte1(84) = 69
        VNIByte2(84) = 217
        VNIByte1(85) = 69
        VNIByte2(85) = 216
        VNIByte1(86) = 69
        VNIByte2(86) = 219
        VNIByte1(87) = 69
        VNIByte2(87) = 213
        VNIByte1(88) = 69
        VNIByte2(88) = 207
        VNIByte1(89) = 69
        VNIByte2(89) = 194
        VNIByte1(90) = 69
        VNIByte2(90) = 193
        VNIByte1(91) = 69
        VNIByte2(91) = 192
        VNIByte1(92) = 69
        VNIByte2(92) = 197
        VNIByte1(93) = 69
        VNIByte2(93) = 195
        VNIByte1(94) = 69
        VNIByte2(94) = 196
        VNIByte1(95) = 205
        VNIByte2(95) = 0
        VNIByte1(96) = 204
        VNIByte2(96) = 0
        VNIByte1(97) = 198
        VNIByte2(97) = 0
        VNIByte1(98) = 211
        VNIByte2(98) = 0
        VNIByte1(99) = 210
        VNIByte2(99) = 0
        VNIByte1(100) = 79
        VNIByte2(100) = 217
        VNIByte1(101) = 79
        VNIByte2(101) = 216
        VNIByte1(102) = 79
        VNIByte2(102) = 219
        VNIByte1(103) = 79
        VNIByte2(103) = 213
        VNIByte1(104) = 79
        VNIByte2(104) = 207
        VNIByte1(105) = 79
        VNIByte2(105) = 194
        VNIByte1(106) = 79
        VNIByte2(106) = 193
        VNIByte1(107) = 79
        VNIByte2(107) = 192
        VNIByte1(108) = 79
        VNIByte2(108) = 197
        VNIByte1(109) = 79
        VNIByte2(109) = 195
        VNIByte1(110) = 79
        VNIByte2(110) = 196
        VNIByte1(111) = 212
        VNIByte2(111) = 0
        VNIByte1(112) = 212
        VNIByte2(112) = 217
        VNIByte1(113) = 212
        VNIByte2(113) = 216
        VNIByte1(114) = 212
        VNIByte2(114) = 219
        VNIByte1(115) = 212
        VNIByte2(115) = 213
        VNIByte1(116) = 212
        VNIByte2(116) = 207
        VNIByte1(117) = 85
        VNIByte2(117) = 217
        VNIByte1(118) = 85
        VNIByte2(118) = 216
        VNIByte1(119) = 85
        VNIByte2(119) = 219
        VNIByte1(120) = 85
        VNIByte2(120) = 213
        VNIByte1(121) = 85
        VNIByte2(121) = 207
        VNIByte1(122) = 214
        VNIByte2(122) = 0
        VNIByte1(123) = 214
        VNIByte2(123) = 217
        VNIByte1(124) = 214
        VNIByte2(124) = 216
        VNIByte1(125) = 214
        VNIByte2(125) = 219
        VNIByte1(126) = 214
        VNIByte2(126) = 213
        VNIByte1(127) = 214
        VNIByte2(127) = 207
        VNIByte1(128) = 89
        VNIByte2(128) = 217
        VNIByte1(129) = 89
        VNIByte2(129) = 216
        VNIByte1(130) = 89
        VNIByte2(130) = 219
        VNIByte1(131) = 89
        VNIByte2(131) = 213
        VNIByte1(132) = 206
        VNIByte2(132) = 0
        VNIByte1(133) = 209
        VNIByte2(133) = 0

        For lngCtr = 0 To 133
            If VNIByte2(lngCtr) = 0 Then
                VNIMap(lngCtr) = ChrW(VNIByte1(lngCtr))
            Else
                VNIMap(lngCtr) = ChrW(VNIByte1(lngCtr)) & ChrW(VNIByte2(lngCtr))
            End If
        Next

        ' Tao bang cua PCW-Times
        PCWByte1(0) = 97
        PCWByte2(0) = 225
        PCWByte1(1) = 97
        PCWByte2(1) = 226
        PCWByte1(2) = 97
        PCWByte2(2) = 227
        PCWByte1(3) = 97
        PCWByte2(3) = 228
        PCWByte1(4) = 97
        PCWByte2(4) = 229
        PCWByte1(5) = 234
        PCWByte2(5) = 0
        PCWByte1(6) = 234
        PCWByte2(6) = 235
        PCWByte1(7) = 234
        PCWByte2(7) = 236
        PCWByte1(8) = 234
        PCWByte2(8) = 237
        PCWByte1(9) = 234
        PCWByte2(9) = 238
        PCWByte1(10) = 234
        PCWByte2(10) = 229
        PCWByte1(11) = 249
        PCWByte2(11) = 0
        PCWByte1(12) = 249
        PCWByte2(12) = 230
        PCWByte1(13) = 249
        PCWByte2(13) = 231
        PCWByte1(14) = 249
        PCWByte2(14) = 232
        PCWByte1(15) = 249
        PCWByte2(15) = 233
        PCWByte1(16) = 249
        PCWByte2(16) = 229
        PCWByte1(17) = 101
        PCWByte2(17) = 225
        PCWByte1(18) = 101
        PCWByte2(18) = 226
        PCWByte1(19) = 101
        PCWByte2(19) = 227
        PCWByte1(20) = 101
        PCWByte2(20) = 228
        PCWByte1(21) = 101
        PCWByte2(21) = 229
        PCWByte1(22) = 239
        PCWByte2(22) = 0
        PCWByte1(23) = 239
        PCWByte2(23) = 235
        PCWByte1(24) = 239
        PCWByte2(24) = 236
        PCWByte1(25) = 239
        PCWByte2(25) = 237
        PCWByte1(26) = 239
        PCWByte2(26) = 238
        PCWByte1(27) = 239
        PCWByte2(27) = 229
        PCWByte1(28) = 241
        PCWByte2(28) = 0
        PCWByte1(29) = 242
        PCWByte2(29) = 0
        PCWByte1(30) = 243
        PCWByte2(30) = 0
        PCWByte1(31) = 244
        PCWByte2(31) = 0
        PCWByte1(32) = 245
        PCWByte2(32) = 0
        PCWByte1(33) = 111
        PCWByte2(33) = 225
        PCWByte1(34) = 111
        PCWByte2(34) = 226
        PCWByte1(35) = 111
        PCWByte2(35) = 227
        PCWByte1(36) = 111
        PCWByte2(36) = 228
        PCWByte1(37) = 111
        PCWByte2(37) = 229
        PCWByte1(38) = 246
        PCWByte2(38) = 0
        PCWByte1(39) = 246
        PCWByte2(39) = 235
        PCWByte1(40) = 246
        PCWByte2(40) = 236
        PCWByte1(41) = 246
        PCWByte2(41) = 237
        PCWByte1(42) = 246
        PCWByte2(42) = 238
        PCWByte1(43) = 246
        PCWByte2(43) = 229
        PCWByte1(44) = 250
        PCWByte2(44) = 0
        PCWByte1(45) = 250
        PCWByte2(45) = 225
        PCWByte1(46) = 250
        PCWByte2(46) = 226
        PCWByte1(47) = 250
        PCWByte2(47) = 227
        PCWByte1(48) = 250
        PCWByte2(48) = 228
        PCWByte1(49) = 250
        PCWByte2(49) = 229
        PCWByte1(50) = 117
        PCWByte2(50) = 225
        PCWByte1(51) = 117
        PCWByte2(51) = 226
        PCWByte1(52) = 117
        PCWByte2(52) = 227
        PCWByte1(53) = 117
        PCWByte2(53) = 228
        PCWByte1(54) = 117
        PCWByte2(54) = 229
        PCWByte1(55) = 251
        PCWByte2(55) = 0
        PCWByte1(56) = 251
        PCWByte2(56) = 225
        PCWByte1(57) = 251
        PCWByte2(57) = 226
        PCWByte1(58) = 251
        PCWByte2(58) = 227
        PCWByte1(59) = 251
        PCWByte2(59) = 228
        PCWByte1(60) = 251
        PCWByte2(60) = 229
        PCWByte1(61) = 121
        PCWByte2(61) = 225
        PCWByte1(62) = 121
        PCWByte2(62) = 226
        PCWByte1(63) = 121
        PCWByte2(63) = 227
        PCWByte1(64) = 121
        PCWByte2(64) = 228
        PCWByte1(65) = 121
        PCWByte2(65) = 197
        PCWByte1(66) = 224
        PCWByte2(66) = 0
        PCWByte1(67) = 65
        PCWByte2(67) = 193
        PCWByte1(68) = 65
        PCWByte2(68) = 194
        PCWByte1(69) = 65
        PCWByte2(69) = 195
        PCWByte1(70) = 65
        PCWByte2(70) = 196
        PCWByte1(71) = 65
        PCWByte2(71) = 197
        PCWByte1(72) = 202
        PCWByte2(72) = 0
        PCWByte1(73) = 202
        PCWByte2(73) = 203
        PCWByte1(74) = 202
        PCWByte2(74) = 204
        PCWByte1(75) = 202
        PCWByte2(75) = 205
        PCWByte1(76) = 202
        PCWByte2(76) = 206
        PCWByte1(77) = 202
        PCWByte2(77) = 197
        PCWByte1(78) = 217
        PCWByte2(78) = 0
        PCWByte1(79) = 217
        PCWByte2(79) = 198
        PCWByte1(80) = 217
        PCWByte2(80) = 199
        PCWByte1(81) = 217
        PCWByte2(81) = 200
        PCWByte1(82) = 217
        PCWByte2(82) = 201
        PCWByte1(83) = 217
        PCWByte2(83) = 197
        PCWByte1(84) = 69
        PCWByte2(84) = 193
        PCWByte1(85) = 69
        PCWByte2(85) = 194
        PCWByte1(86) = 69
        PCWByte2(86) = 195
        PCWByte1(87) = 69
        PCWByte2(87) = 196
        PCWByte1(88) = 69
        PCWByte2(88) = 197
        PCWByte1(89) = 207
        PCWByte2(89) = 0
        PCWByte1(90) = 207
        PCWByte2(90) = 203
        PCWByte1(91) = 207
        PCWByte2(91) = 204
        PCWByte1(92) = 207
        PCWByte2(92) = 205
        PCWByte1(93) = 207
        PCWByte2(93) = 206
        PCWByte1(94) = 207
        PCWByte2(94) = 197
        PCWByte1(95) = 209
        PCWByte2(95) = 0
        PCWByte1(96) = 210
        PCWByte2(96) = 0
        PCWByte1(97) = 211
        PCWByte2(97) = 0
        PCWByte1(98) = 212
        PCWByte2(98) = 0
        PCWByte1(99) = 213
        PCWByte2(99) = 0
        PCWByte1(100) = 79
        PCWByte2(100) = 193
        PCWByte1(101) = 79
        PCWByte2(101) = 194
        PCWByte1(102) = 79
        PCWByte2(102) = 195
        PCWByte1(103) = 79
        PCWByte2(103) = 196
        PCWByte1(104) = 79
        PCWByte2(104) = 197
        PCWByte1(105) = 214
        PCWByte2(105) = 0
        PCWByte1(106) = 214
        PCWByte2(106) = 203
        PCWByte1(107) = 214
        PCWByte2(107) = 204
        PCWByte1(108) = 214
        PCWByte2(108) = 205
        PCWByte1(109) = 214
        PCWByte2(109) = 206
        PCWByte1(110) = 214
        PCWByte2(110) = 197
        '
        PCWByte1(111) = 218
        PCWByte2(111) = 0
        PCWByte1(112) = 218
        PCWByte2(112) = 193
        PCWByte1(113) = 218
        PCWByte2(113) = 194
        PCWByte1(114) = 218
        PCWByte2(114) = 195
        PCWByte1(115) = 218
        PCWByte2(115) = 196
        PCWByte1(116) = 218
        PCWByte2(116) = 197
        PCWByte1(117) = 85
        PCWByte2(117) = 193
        PCWByte1(118) = 85
        PCWByte2(118) = 194
        PCWByte1(119) = 85
        PCWByte2(119) = 195
        PCWByte1(120) = 85
        PCWByte2(120) = 196
        PCWByte1(121) = 85
        PCWByte2(121) = 197
        PCWByte1(122) = 219
        PCWByte2(122) = 0
        PCWByte1(123) = 219
        PCWByte2(123) = 193
        PCWByte1(124) = 219
        PCWByte2(124) = 194
        PCWByte1(125) = 219
        PCWByte2(125) = 195
        PCWByte1(126) = 219
        PCWByte2(126) = 196
        PCWByte1(127) = 219
        PCWByte2(127) = 197
        PCWByte1(128) = 89
        PCWByte2(128) = 193
        PCWByte1(129) = 89
        PCWByte2(129) = 194
        PCWByte1(130) = 89
        PCWByte2(130) = 195
        PCWByte1(131) = 89
        PCWByte2(131) = 196
        PCWByte1(132) = 89
        PCWByte2(132) = 197
        PCWByte1(133) = 192
        PCWByte2(133) = 0

        For lngCtr = 0 To 133
            If PCWByte2(lngCtr) = 0 Then
                PCWMap(lngCtr) = ChrW(PCWByte1(lngCtr))
            Else
                PCWMap(lngCtr) = ChrW(PCWByte1(lngCtr)) & ChrW(PCWByte2(lngCtr))
            End If
        Next

        ' ABC
        ABCByte(0) = 184
        ABCByte(1) = 181
        ABCByte(2) = 182
        ABCByte(3) = 183
        ABCByte(4) = 185
        ABCByte(5) = 169
        ABCByte(6) = 202
        ABCByte(7) = 199
        ABCByte(8) = 200
        ABCByte(9) = 201
        ABCByte(10) = 203
        ABCByte(11) = 168
        ABCByte(12) = 190
        ABCByte(13) = 187
        ABCByte(14) = 188
        ABCByte(15) = 189
        ABCByte(16) = 198
        ABCByte(17) = 208
        ABCByte(18) = 204
        ABCByte(19) = 206
        ABCByte(20) = 207
        ABCByte(21) = 209
        ABCByte(22) = 170
        ABCByte(23) = 213
        ABCByte(24) = 210
        ABCByte(25) = 211
        ABCByte(26) = 212
        ABCByte(27) = 214
        ABCByte(28) = 221
        ABCByte(29) = 215
        ABCByte(30) = 216
        ABCByte(31) = 220
        ABCByte(32) = 222
        ABCByte(33) = 227
        ABCByte(34) = 223
        ABCByte(35) = 225
        ABCByte(36) = 226
        ABCByte(37) = 228
        ABCByte(38) = 171
        ABCByte(39) = 232
        ABCByte(40) = 229
        ABCByte(41) = 230
        ABCByte(42) = 231
        ABCByte(43) = 233
        ABCByte(44) = 172
        ABCByte(45) = 237
        ABCByte(46) = 234
        ABCByte(47) = 235
        ABCByte(48) = 236
        ABCByte(49) = 238
        ABCByte(50) = 243
        ABCByte(51) = 239
        ABCByte(52) = 241
        ABCByte(53) = 242
        ABCByte(54) = 244
        ABCByte(55) = 173
        ABCByte(56) = 248
        ABCByte(57) = 245
        ABCByte(58) = 246
        ABCByte(59) = 247
        ABCByte(60) = 249
        ABCByte(61) = 253
        ABCByte(62) = 250
        ABCByte(63) = 251
        ABCByte(64) = 252
        ABCByte(65) = 254
        ABCByte(66) = 174
        ABCByte(67) = 184
        ABCByte(68) = 181
        ABCByte(69) = 182
        ABCByte(70) = 183
        ABCByte(71) = 185
        ABCByte(72) = 162
        ABCByte(73) = 202
        ABCByte(74) = 199
        ABCByte(75) = 200
        ABCByte(76) = 201
        ABCByte(77) = 203
        ABCByte(78) = 161
        ABCByte(79) = 190
        ABCByte(80) = 187
        ABCByte(81) = 188
        ABCByte(82) = 189
        ABCByte(83) = 198
        ABCByte(84) = 208
        ABCByte(85) = 204
        ABCByte(86) = 206
        ABCByte(87) = 207
        ABCByte(88) = 209
        ABCByte(89) = 163
        ABCByte(90) = 213
        ABCByte(91) = 210
        ABCByte(92) = 211
        ABCByte(93) = 212
        ABCByte(94) = 214
        ABCByte(95) = 221
        ABCByte(96) = 215
        ABCByte(97) = 216
        ABCByte(98) = 220
        ABCByte(99) = 222
        ABCByte(100) = 227
        ABCByte(101) = 223
        ABCByte(102) = 225
        ABCByte(103) = 226
        ABCByte(104) = 228
        ABCByte(105) = 164
        ABCByte(106) = 232
        ABCByte(107) = 229
        ABCByte(108) = 230
        ABCByte(109) = 231
        ABCByte(110) = 233
        ABCByte(111) = 165
        ABCByte(112) = 237
        ABCByte(113) = 234
        ABCByte(114) = 235
        ABCByte(115) = 236
        ABCByte(116) = 238
        ABCByte(117) = 243
        ABCByte(118) = 239
        ABCByte(119) = 241
        ABCByte(120) = 242
        ABCByte(121) = 244
        ABCByte(122) = 166
        ABCByte(123) = 248
        ABCByte(124) = 245
        ABCByte(125) = 246
        ABCByte(126) = 247
        ABCByte(127) = 249
        ABCByte(128) = 253
        ABCByte(129) = 250
        ABCByte(130) = 251
        ABCByte(131) = 252
        ABCByte(132) = 254
        ABCByte(133) = 167
        For lngCtr = 0 To 133
            ABCMap(lngCtr) = ChrW(ABCByte(lngCtr))
        Next

        ' VietWare
        VIWByte1(0) = 97
        VIWByte2(0) = 239
        VIWByte1(1) = 97
        VIWByte2(1) = 236
        VIWByte1(2) = 97
        VIWByte2(2) = 237
        VIWByte1(3) = 97
        VIWByte2(3) = 238
        VIWByte1(4) = 97
        VIWByte2(4) = 251
        VIWByte1(5) = 225
        VIWByte2(5) = 0
        VIWByte1(6) = 225
        VIWByte2(6) = 250
        VIWByte1(7) = 225
        VIWByte2(7) = 246
        VIWByte1(8) = 225
        VIWByte2(8) = 248
        VIWByte1(9) = 225
        VIWByte2(9) = 249
        VIWByte1(10) = 225
        VIWByte2(10) = 251
        VIWByte1(11) = 224
        VIWByte2(11) = 0
        VIWByte1(12) = 224
        VIWByte2(12) = 245
        VIWByte1(13) = 224
        VIWByte2(13) = 242
        VIWByte1(14) = 224
        VIWByte2(14) = 243
        VIWByte1(15) = 224
        VIWByte2(15) = 244
        VIWByte1(16) = 224
        VIWByte2(16) = 251
        VIWByte1(17) = 101
        VIWByte2(17) = 239
        VIWByte1(18) = 101
        VIWByte2(18) = 236
        VIWByte1(19) = 101
        VIWByte2(19) = 237
        VIWByte1(20) = 101
        VIWByte2(20) = 238
        VIWByte1(21) = 101
        VIWByte2(21) = 251
        VIWByte1(22) = 227
        VIWByte2(22) = 0
        VIWByte1(23) = 227
        VIWByte2(23) = 250
        VIWByte1(24) = 227
        VIWByte2(24) = 246
        VIWByte1(25) = 227
        VIWByte2(25) = 248
        VIWByte1(26) = 227
        VIWByte2(26) = 249
        VIWByte1(27) = 227
        VIWByte2(27) = 251
        VIWByte1(28) = 234
        VIWByte2(28) = 0
        VIWByte1(29) = 231
        VIWByte2(29) = 0
        VIWByte1(30) = 232
        VIWByte2(30) = 0
        VIWByte1(31) = 233
        VIWByte2(31) = 0
        VIWByte1(32) = 235
        VIWByte2(32) = 0
        VIWByte1(33) = 111
        VIWByte2(33) = 239
        VIWByte1(34) = 111
        VIWByte2(34) = 236
        VIWByte1(35) = 111
        VIWByte2(35) = 237
        VIWByte1(36) = 111
        VIWByte2(36) = 238
        VIWByte1(37) = 111
        VIWByte2(37) = 252
        VIWByte1(38) = 228
        VIWByte2(38) = 0
        VIWByte1(39) = 228
        VIWByte2(39) = 250
        VIWByte1(40) = 228
        VIWByte2(40) = 246
        VIWByte1(41) = 228
        VIWByte2(41) = 248
        VIWByte1(42) = 228
        VIWByte2(42) = 249
        VIWByte1(43) = 228
        VIWByte2(43) = 252
        VIWByte1(44) = 229
        VIWByte2(44) = 0
        VIWByte1(45) = 229
        VIWByte2(45) = 239
        VIWByte1(46) = 229
        VIWByte2(46) = 236
        VIWByte1(47) = 229
        VIWByte2(47) = 237
        VIWByte1(48) = 229
        VIWByte2(48) = 238
        VIWByte1(49) = 229
        VIWByte2(49) = 252
        VIWByte1(50) = 117
        VIWByte2(50) = 239
        VIWByte1(51) = 117
        VIWByte2(51) = 236
        VIWByte1(52) = 117
        VIWByte2(52) = 237
        VIWByte1(53) = 117
        VIWByte2(53) = 238
        VIWByte1(54) = 117
        VIWByte2(54) = 251
        VIWByte1(55) = 230
        VIWByte2(55) = 0
        VIWByte1(56) = 230
        VIWByte2(56) = 239
        VIWByte1(57) = 230
        VIWByte2(57) = 236
        VIWByte1(58) = 230
        VIWByte2(58) = 237
        VIWByte1(59) = 230
        VIWByte2(59) = 238
        VIWByte1(60) = 230
        VIWByte2(60) = 251
        VIWByte1(61) = 121
        VIWByte2(61) = 239
        VIWByte1(62) = 121
        VIWByte2(62) = 236
        VIWByte1(63) = 121
        VIWByte2(63) = 237
        VIWByte1(64) = 121
        VIWByte2(64) = 238
        VIWByte1(65) = 121
        VIWByte2(65) = 241
        VIWByte1(66) = 226
        VIWByte2(66) = 0
        VIWByte1(67) = 65
        VIWByte2(67) = 207
        VIWByte1(68) = 65
        VIWByte2(68) = 204
        VIWByte1(69) = 65
        VIWByte2(69) = 205
        VIWByte1(70) = 65
        VIWByte2(70) = 206
        VIWByte1(71) = 65
        VIWByte2(71) = 219
        VIWByte1(72) = 193
        VIWByte2(72) = 0
        VIWByte1(73) = 193
        VIWByte2(73) = 218
        VIWByte1(74) = 193
        VIWByte2(74) = 214
        VIWByte1(75) = 193
        VIWByte2(75) = 216
        VIWByte1(76) = 193
        VIWByte2(76) = 217
        VIWByte1(77) = 193
        VIWByte2(77) = 219
        VIWByte1(78) = 192
        VIWByte2(78) = 0
        VIWByte1(79) = 192
        VIWByte2(79) = 213
        VIWByte1(80) = 192
        VIWByte2(80) = 210
        VIWByte1(81) = 192
        VIWByte2(81) = 211
        VIWByte1(82) = 192
        VIWByte2(82) = 212
        VIWByte1(83) = 192
        VIWByte2(83) = 219
        VIWByte1(84) = 69
        VIWByte2(84) = 207
        VIWByte1(85) = 69
        VIWByte2(85) = 204
        VIWByte1(86) = 69
        VIWByte2(86) = 205
        VIWByte1(87) = 69
        VIWByte2(87) = 206
        VIWByte1(88) = 69
        VIWByte2(88) = 219
        VIWByte1(89) = 195
        VIWByte2(89) = 0
        VIWByte1(90) = 195
        VIWByte2(90) = 218
        VIWByte1(91) = 195
        VIWByte2(91) = 214
        VIWByte1(92) = 195
        VIWByte2(92) = 216
        VIWByte1(93) = 195
        VIWByte2(93) = 217
        VIWByte1(94) = 195
        VIWByte2(94) = 219
        VIWByte1(95) = 202
        VIWByte2(95) = 0
        VIWByte1(96) = 199
        VIWByte2(96) = 0
        VIWByte1(97) = 200
        VIWByte2(97) = 0
        VIWByte1(98) = 201
        VIWByte2(98) = 0
        VIWByte1(99) = 203
        VIWByte2(99) = 0
        VIWByte1(100) = 79
        VIWByte2(100) = 207
        VIWByte1(101) = 79
        VIWByte2(101) = 204
        VIWByte1(102) = 79
        VIWByte2(102) = 205
        VIWByte1(103) = 79
        VIWByte2(103) = 206
        VIWByte1(104) = 79
        VIWByte2(104) = 220
        VIWByte1(105) = 196
        VIWByte2(105) = 0
        VIWByte1(106) = 196
        VIWByte2(106) = 218
        VIWByte1(107) = 196
        VIWByte2(107) = 214
        VIWByte1(108) = 196
        VIWByte2(108) = 216
        VIWByte1(109) = 196
        VIWByte2(109) = 217
        VIWByte1(110) = 196
        VIWByte2(110) = 220
        VIWByte1(111) = 197
        VIWByte2(111) = 0
        VIWByte1(112) = 197
        VIWByte2(112) = 207
        VIWByte1(113) = 197
        VIWByte2(113) = 204
        VIWByte1(114) = 197
        VIWByte2(114) = 205
        VIWByte1(115) = 197
        VIWByte2(115) = 206
        VIWByte1(116) = 197
        VIWByte2(116) = 220
        VIWByte1(117) = 85
        VIWByte2(117) = 207
        VIWByte1(118) = 85
        VIWByte2(118) = 204
        VIWByte1(119) = 85
        VIWByte2(119) = 205
        VIWByte1(120) = 85
        VIWByte2(120) = 206
        VIWByte1(121) = 85
        VIWByte2(121) = 219
        VIWByte1(122) = 198
        VIWByte2(122) = 0
        VIWByte1(123) = 198
        VIWByte2(123) = 207
        VIWByte1(124) = 198
        VIWByte2(124) = 204
        VIWByte1(125) = 198
        VIWByte2(125) = 205
        VIWByte1(126) = 198
        VIWByte2(126) = 206
        VIWByte1(127) = 198
        VIWByte2(127) = 219
        VIWByte1(128) = 89
        VIWByte2(128) = 207
        VIWByte1(129) = 89
        VIWByte2(129) = 204
        VIWByte1(130) = 89
        VIWByte2(130) = 205
        VIWByte1(131) = 89
        VIWByte2(131) = 206
        VIWByte1(132) = 89
        VIWByte2(132) = 209
        VIWByte1(133) = 194
        VIWByte2(133) = 0
        For lngCtr = 0 To 133
            If VIWByte2(lngCtr) = 0 Then
                VIWMap(lngCtr) = ChrW(VIWByte1(lngCtr))
            Else
                VIWMap(lngCtr) = ChrW(VIWByte1(lngCtr)) & ChrW(VIWByte2(lngCtr))
            End If
        Next

        ' Phan tao ma khong dau
        KhongDauMap(0) = "a"
        KhongDauMap(1) = "a"
        KhongDauMap(2) = "a"
        KhongDauMap(3) = "a"
        KhongDauMap(4) = "a"
        KhongDauMap(5) = "a"
        KhongDauMap(6) = "a"
        KhongDauMap(7) = "a"
        KhongDauMap(8) = "a"
        KhongDauMap(9) = "a"
        KhongDauMap(10) = "a"
        KhongDauMap(11) = "a"
        KhongDauMap(12) = "a"
        KhongDauMap(13) = "a"
        KhongDauMap(14) = "a"
        KhongDauMap(15) = "a"
        KhongDauMap(16) = "a"
        KhongDauMap(17) = "e"
        KhongDauMap(18) = "e"
        KhongDauMap(19) = "e"
        KhongDauMap(20) = "e"
        KhongDauMap(21) = "e"
        KhongDauMap(22) = "e"
        KhongDauMap(23) = "e"
        KhongDauMap(24) = "e"
        KhongDauMap(25) = "e"
        KhongDauMap(26) = "e"
        KhongDauMap(27) = "e"
        KhongDauMap(28) = "i"
        KhongDauMap(29) = "i"
        KhongDauMap(30) = "i"
        KhongDauMap(31) = "i"
        KhongDauMap(32) = "i"
        KhongDauMap(33) = "o"
        KhongDauMap(34) = "o"
        KhongDauMap(35) = "o"
        KhongDauMap(36) = "o"
        KhongDauMap(37) = "o"
        KhongDauMap(38) = "o"
        KhongDauMap(39) = "o"
        KhongDauMap(40) = "o"
        KhongDauMap(41) = "o"
        KhongDauMap(42) = "o"
        KhongDauMap(43) = "o"
        KhongDauMap(44) = "o"
        KhongDauMap(45) = "o"
        KhongDauMap(46) = "o"
        KhongDauMap(47) = "o"
        KhongDauMap(48) = "o"
        KhongDauMap(49) = "o"
        KhongDauMap(50) = "u"
        KhongDauMap(51) = "u"
        KhongDauMap(52) = "u"
        KhongDauMap(53) = "u"
        KhongDauMap(54) = "u"
        KhongDauMap(55) = "u"
        KhongDauMap(56) = "u"
        KhongDauMap(57) = "u"
        KhongDauMap(58) = "u"
        KhongDauMap(59) = "u"
        KhongDauMap(60) = "u"
        KhongDauMap(61) = "y"
        KhongDauMap(62) = "y"
        KhongDauMap(63) = "y"
        KhongDauMap(64) = "y"
        KhongDauMap(65) = "y"
        KhongDauMap(66) = "d"
        KhongDauMap(67) = "A"
        KhongDauMap(68) = "A"
        KhongDauMap(69) = "A"
        KhongDauMap(70) = "A"
        KhongDauMap(71) = "A"
        KhongDauMap(72) = "A"
        KhongDauMap(73) = "A"
        KhongDauMap(74) = "A"
        KhongDauMap(75) = "A"
        KhongDauMap(76) = "A"
        KhongDauMap(77) = "A"
        KhongDauMap(78) = "A"
        KhongDauMap(79) = "A"
        KhongDauMap(80) = "A"
        KhongDauMap(81) = "A"
        KhongDauMap(82) = "A"
        KhongDauMap(83) = "A"
        KhongDauMap(84) = "E"
        KhongDauMap(85) = "E"
        KhongDauMap(86) = "E"
        KhongDauMap(87) = "E"
        KhongDauMap(88) = "E"
        KhongDauMap(89) = "E"
        KhongDauMap(90) = "E"
        KhongDauMap(91) = "E"
        KhongDauMap(92) = "E"
        KhongDauMap(93) = "E"
        KhongDauMap(94) = "E"
        KhongDauMap(95) = "I"
        KhongDauMap(96) = "I"
        KhongDauMap(97) = "I"
        KhongDauMap(98) = "I"
        KhongDauMap(99) = "I"
        KhongDauMap(100) = "O"
        KhongDauMap(101) = "O"
        KhongDauMap(102) = "O"
        KhongDauMap(103) = "O"
        KhongDauMap(104) = "O"
        KhongDauMap(105) = "O"
        KhongDauMap(106) = "O"
        KhongDauMap(107) = "O"
        KhongDauMap(108) = "O"
        KhongDauMap(109) = "O"
        KhongDauMap(110) = "O"
        KhongDauMap(111) = "O"
        KhongDauMap(112) = "O"
        KhongDauMap(113) = "O"
        KhongDauMap(114) = "O"
        KhongDauMap(115) = "O"
        KhongDauMap(116) = "O"
        KhongDauMap(117) = "U"
        KhongDauMap(118) = "U"
        KhongDauMap(119) = "U"
        KhongDauMap(120) = "U"
        KhongDauMap(121) = "U"
        KhongDauMap(122) = "U"
        KhongDauMap(123) = "U"
        KhongDauMap(124) = "U"
        KhongDauMap(125) = "U"
        KhongDauMap(126) = "U"
        KhongDauMap(127) = "U"
        KhongDauMap(128) = "Y"
        KhongDauMap(129) = "Y"
        KhongDauMap(130) = "Y"
        KhongDauMap(131) = "Y"
        KhongDauMap(132) = "Y"
        KhongDauMap(133) = "D"
        KhongDauMap(134) = ""
        KhongDauMap(135) = ""

        ' Tao ma font Viet ware F
        VietWareFByte(0) = 192
        VietWareFByte(1) = 170
        VietWareFByte(2) = 182
        VietWareFByte(3) = 186
        VietWareFByte(4) = 193
        VietWareFByte(5) = 161
        VietWareFByte(6) = 202
        VietWareFByte(7) = 199
        VietWareFByte(8) = 200
        VietWareFByte(9) = 201
        VietWareFByte(10) = 203
        VietWareFByte(11) = 376
        VietWareFByte(12) = 197
        VietWareFByte(13) = 194
        VietWareFByte(14) = 195
        VietWareFByte(15) = 196
        VietWareFByte(16) = 198
        VietWareFByte(17) = 207
        VietWareFByte(18) = 204
        VietWareFByte(19) = 205
        VietWareFByte(20) = 206
        VietWareFByte(21) = 209
        VietWareFByte(22) = 163
        VietWareFByte(23) = 213
        VietWareFByte(24) = 210
        VietWareFByte(25) = 211
        VietWareFByte(26) = 212
        VietWareFByte(27) = 214
        VietWareFByte(28) = 219
        VietWareFByte(29) = 216
        VietWareFByte(30) = 217
        VietWareFByte(31) = 218
        VietWareFByte(32) = 220
        VietWareFByte(33) = 226
        VietWareFByte(34) = 223
        VietWareFByte(35) = 224
        VietWareFByte(36) = 225
        VietWareFByte(37) = 227
        VietWareFByte(38) = 164
        VietWareFByte(39) = 231
        VietWareFByte(40) = 228
        VietWareFByte(41) = 229
        VietWareFByte(42) = 230
        VietWareFByte(43) = 232
        VietWareFByte(44) = 165
        VietWareFByte(45) = 236
        VietWareFByte(46) = 233
        VietWareFByte(47) = 234
        VietWareFByte(48) = 235
        VietWareFByte(49) = 237
        VietWareFByte(50) = 242
        VietWareFByte(51) = 238
        VietWareFByte(52) = 239
        VietWareFByte(53) = 241
        VietWareFByte(54) = 243
        VietWareFByte(55) = 167
        VietWareFByte(56) = 247
        VietWareFByte(57) = 244
        VietWareFByte(58) = 245
        VietWareFByte(59) = 246
        VietWareFByte(60) = 248
        VietWareFByte(61) = 252
        VietWareFByte(62) = 249
        VietWareFByte(63) = 250
        VietWareFByte(64) = 251
        VietWareFByte(65) = 255
        VietWareFByte(66) = 162
        VietWareFByte(67) = 192
        VietWareFByte(68) = 170
        VietWareFByte(69) = 182
        VietWareFByte(70) = 186
        VietWareFByte(71) = 193
        VietWareFByte(72) = 8212
        VietWareFByte(73) = 202
        VietWareFByte(74) = 199
        VietWareFByte(75) = 200
        VietWareFByte(76) = 201
        VietWareFByte(77) = 203
        VietWareFByte(78) = 8211
        VietWareFByte(79) = 197
        VietWareFByte(80) = 194
        VietWareFByte(81) = 195
        VietWareFByte(82) = 196
        VietWareFByte(83) = 198
        VietWareFByte(84) = 207
        VietWareFByte(85) = 204
        VietWareFByte(86) = 205
        VietWareFByte(87) = 206
        VietWareFByte(88) = 209
        VietWareFByte(89) = 8482
        VietWareFByte(90) = 213
        VietWareFByte(91) = 210
        VietWareFByte(92) = 211
        VietWareFByte(93) = 212
        VietWareFByte(94) = 214
        VietWareFByte(95) = 219
        VietWareFByte(96) = 216
        VietWareFByte(97) = 217
        VietWareFByte(98) = 218
        VietWareFByte(99) = 220
        VietWareFByte(100) = 226
        VietWareFByte(101) = 223
        VietWareFByte(102) = 224
        VietWareFByte(103) = 225
        VietWareFByte(104) = 227
        VietWareFByte(105) = 353
        VietWareFByte(106) = 231
        VietWareFByte(107) = 228
        VietWareFByte(108) = 229
        VietWareFByte(109) = 230
        VietWareFByte(110) = 232
        VietWareFByte(111) = 8250
        VietWareFByte(112) = 236
        VietWareFByte(113) = 233
        VietWareFByte(114) = 234
        VietWareFByte(115) = 235
        VietWareFByte(116) = 237
        VietWareFByte(117) = 242
        VietWareFByte(118) = 238
        VietWareFByte(119) = 239
        VietWareFByte(120) = 241
        VietWareFByte(121) = 243
        VietWareFByte(122) = 339
        VietWareFByte(123) = 247
        VietWareFByte(124) = 244
        VietWareFByte(125) = 245
        VietWareFByte(126) = 246
        VietWareFByte(127) = 248
        VietWareFByte(128) = 252
        VietWareFByte(129) = 249
        VietWareFByte(130) = 250
        VietWareFByte(131) = 251
        VietWareFByte(132) = 255
        VietWareFByte(133) = 732

        For lngCtr = 0 To 133
            VietWareFMap(lngCtr) = ChrW(VietWareFByte(lngCtr))
        Next
    End Sub

    ' Cach lam:
    ' Doc ky tu dau tien
    ' Anh xa sang ma chi so
    ' Tu ma chi so ung voi ma code
    ' Lap lai cho den het chuoi
    Public Function ConvertFont(ByVal strSource As String, ByVal codeSource As FontType, _
            ByVal codeDest As FontType) As String
        ' Purpose: Ham chuyen doi chuoi strSource tu font codeSource                sang font codeDest
        ' Accepts: strSource: chuoi can chuyen doi _
        'codeSource: ma ban dau _
        'codeDest: ma can chuyen doi
        ' Returns: strDest: chuoi ket qua _
        '<> 0 : ma loi, =0 chuyen doi thanh cong
        Dim strDestTemp As String
        Dim strTemp As String
        Dim lngTemp As Long
        Dim lngTemp1 As Long
        Dim lngTemp2 As Long
        Dim lngItemCtr As Long ' Chi so dau tien
        Dim lngFlag As Long  ' Xac dinh ma
        Dim lngContinue As Long '  chi so trong truong hop nam trong day va ma chi 1 byte
        Dim blnFlag As Boolean
        On Error GoTo eh

        If codeSource = codeDest Then
            Return strSource
        End If
        If strSource = "" Then
            Return ""
        End If

        Call Init()
        Select Case codeSource
            Case 0 ' UNI
                Do Until strSource = ""
                    strTemp = Left(strSource, 1)
                    lngTemp = AscW(strTemp)
                    For lngItemCtr = 0 To 133
                        If lngTemp = UniByte(lngItemCtr) Then Exit For
                    Next

                    strSource = Mid(strSource, 2)
                    If lngItemCtr < 134 Then
                        strDestTemp = strDestTemp & OutCode(lngItemCtr, codeDest)
                    Else
                        strDestTemp = strDestTemp & strTemp
                    End If
                Loop

            Case 1 'VNI
                Do Until strSource = ""
                    blnFlag = False
                    lngContinue = 0
                    lngFlag = 134
                    strTemp = Left(strSource, 1)
                    lngTemp1 = Asc(strTemp)

                    If Len(strSource) > 1 Then
                        lngTemp2 = AscW(Mid(strSource, 2, 1))
                    Else
                        lngTemp2 = -1
                    End If

                    For lngItemCtr = 0 To 133
                        If lngTemp1 = VNIByte1(lngItemCtr) Then
                            lngFlag = lngItemCtr
                            If lngTemp2 = VNIByte2(lngItemCtr) Then GoTo FOUND
                            If lngTemp2 = 0 Then GoTo FOUND
                            If VNIByte2(lngItemCtr) = 0 Then
                                lngContinue = lngItemCtr
                            End If
                        End If
                    Next
FOUND:
                    If lngItemCtr = 134 And lngFlag = 134 Then  'Khong tim thay
                        strSource = Mid(strSource, 2)
                        strDestTemp = strDestTemp & strTemp
                    ElseIf lngItemCtr = lngFlag Then ' Tim thay mot cap  thoa
                        strSource = Mid(strSource, 3)
                        strDestTemp = strDestTemp & OutCode(lngItemCtr, codeDest)
                    ElseIf lngItemCtr = 134 And lngFlag > 0 And lngContinue <> 0 Then   ' Tim thay nhung ma con tiep dien
                        strSource = Mid(strSource, 2)
                        strDestTemp = strDestTemp & OutCode(lngContinue, codeDest)
                    ElseIf lngItemCtr = 134 And lngFlag > 0 And lngContinue = 0 Then   ' Khong tim thay
                        strSource = Mid(strSource, 2)
                        strDestTemp = strDestTemp & strTemp
                    End If
                Loop

            Case 2 ' ABC
                Do Until strSource = ""
                    strTemp = Left(strSource, 1)
                    lngTemp = AscW(strTemp)
                    For lngItemCtr = 0 To 133
                        If lngTemp = ABCByte(lngItemCtr) Then Exit For
                    Next

                    strSource = Mid(strSource, 2)

                    If lngItemCtr < 134 Then
                        strDestTemp = strDestTemp & OutCode(lngItemCtr, codeDest)
                    Else
                        strDestTemp = strDestTemp & strTemp
                    End If
                Loop

            Case 3 'VietWare
                Do Until strSource = ""
                    blnFlag = False
                    lngFlag = 134
                    strTemp = Left(strSource, 1)
                    lngTemp1 = Asc(strTemp)
                    If Len(strSource) > 1 Then
                        lngTemp2 = AscW(Mid(strSource, 2, 1))
                    Else
                        lngTemp2 = 0
                    End If
                    For lngItemCtr = 0 To 133
                        If VIWByte1(lngItemCtr) = lngTemp1 Then
                            If lngTemp2 = VIWByte2(lngItemCtr) Then GoTo FOUND_VW
                            If lngTemp2 = 0 Then GoTo FOUND
                            If VIWByte2(lngItemCtr) = 0 Then
                                lngFlag = lngItemCtr
                                blnFlag = True
                            End If
                        End If
                    Next
FOUND_VW:
                    If (lngFlag < 134) And (lngItemCtr = 134) Then lngItemCtr = lngFlag
                    If (lngItemCtr < 134) And VIWByte2(lngItemCtr) <> 0 Then
                        strSource = Mid(strSource, 3)
                    Else
                        strSource = Mid(strSource, 2)
                    End If

                    If lngItemCtr < 134 Then
                        strDestTemp = strDestTemp & OutCode(lngItemCtr, codeDest)
                    Else
                        strDestTemp = strDestTemp & strTemp
                    End If
                Loop

            Case 4 'PCW-Times
                Do Until strSource = ""
                    blnFlag = False
                    lngFlag = 134
                    strTemp = Left(strSource, 1)
                    lngTemp1 = Asc(strTemp)
                    If Len(strSource) > 1 Then
                        lngTemp2 = AscW(Mid(strSource, 2, 1))
                    Else
                        lngTemp2 = 0
                    End If
                    For lngItemCtr = 0 To 133
                        If PCWByte1(lngItemCtr) = lngTemp1 Then
                            If lngTemp2 = PCWByte2(lngItemCtr) Then GoTo FOUNDPCW
                            If lngTemp2 = 0 Then GoTo FOUND
                            If PCWByte2(lngItemCtr) = 0 Then
                                lngFlag = lngItemCtr
                                blnFlag = True
                            End If
                        End If
                    Next
FOUNDPCW:
                    If (lngFlag < 134) And (lngItemCtr = 134) Then lngItemCtr = lngFlag
                    If (lngItemCtr < 134) And PCWByte2(lngItemCtr) <> 0 Then
                        strSource = Mid(strSource, 3)
                    Else
                        strSource = Mid(strSource, 2)
                    End If

                    If lngItemCtr < 134 Then
                        strDestTemp = strDestTemp & OutCode(lngItemCtr, codeDest)
                    Else
                        strDestTemp = strDestTemp & strTemp
                    End If
                Loop

            Case 5 ' VietwareF
                Do Until strSource = ""
                    strTemp = Left(strSource, 1)
                    lngTemp = AscW(strTemp)
                    For lngItemCtr = 0 To 133
                        If lngTemp = VietWareFByte(lngItemCtr) Then Exit For
                    Next

                    strSource = Mid(strSource, 2)

                    If lngItemCtr < 134 Then
                        strDestTemp = strDestTemp & OutCode(lngItemCtr, codeDest)
                    Else
                        strDestTemp = strDestTemp & strTemp
                    End If
                Loop
        End Select

        Return strDestTemp
eh:
        Return ""
    End Function

    Private Function OutCode(ByVal lngPos As Long, ByVal codeDest As FontType) As String
        On Error GoTo eh

        Select Case codeDest
            Case 0 ' UNI
                OutCode = UniMap(lngPos)
            Case 1 'VNI
                OutCode = VNIMap(lngPos)
            Case 2 'ABC
                OutCode = ABCMap(lngPos)
            Case 3 'VietWare
                OutCode = VIWMap(lngPos)
            Case 4 ' PCW-Times
                OutCode = PCWMap(lngPos)
            Case 5 '   chuyen sang khong dau
                OutCode = KhongDauMap(lngPos)
            Case 6 ' VietWareF
                OutCode = VietWareFMap(lngPos)
        End Select
        Exit Function
eh:
        OutCode = Err.Number
    End Function




End Module
