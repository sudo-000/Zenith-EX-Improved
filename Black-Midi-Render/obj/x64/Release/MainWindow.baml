   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  A =Zenith, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Zenith_MIDIZenith_MIDI G CZenithEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG3clr-namespace:ZenithEngine.UI;assembly=ZenithEngineZenithEngine.UI    Zenith_MIDI.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     6sys&clr-namespace:System;assembly=mscorlib     +localclr-namespace:Zenith_MIDI     Bui3clr-namespace:ZenithEngine.UI;assembly=ZenithEngine     -   $��
mainWindow��&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=� � WindowStyle$
 None=� � Title$ Zenith Modded��$	��600��$	��900��$	��900��$	��600�����#171717'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 40Q� � 	Resources  �� MergedDictionaries �� Source#  Zenith_MIDI.AndValueConverter   Key"  Zenith_MIDI.OrValueConverter#  Zenith_MIDI.NotValueConverter��  ��  �� $6 0pack://siteoforigin:,,,/Languages/en/window.xaml?��� $D >pack://application:,,,/ZenithEngine;component/UI/Material.xaml?���   AndValueConverter    OrValueConverter    NotValueConverter  ��staticControlsEnabled  True����previewStartEnabled  	False����previewStopEnabled  	False����unloadEnabled  	False����notPreviewing  True����notRendering  True����
midiLoaded  	False��%    .��]� .+�� .+�� -   $��
midiLoaded��  
midiLoaded#{��  � -   $��midiNotLoaded��{�`�   NotValueConverter	 `� 	Converter#	 [ .1��� 
 �� ElementName$
 
midiLoaded�� �� Path$ 	IsEnabled�� -   $��notRendering��  notRendering#{��  � -   $��notPreviewing��  notPreviewing#{��  � -   $��notPreviewingOrRendering��{�`�   AndValueConverter#	 [ .1��� $
 notPreviewing��$ 	IsEnabled��� $
 notRendering��$ 	IsEnabled�� -   $��previewStartEnabled��{�`� #	 [ .1��� $
 notPreviewing��$ 	IsEnabled��� $
 notRendering��$ 	IsEnabled��� $
 
midiLoaded��$ 	IsEnabled�� -   $��isRendering��{�`� #	 [ .1��� $
 notRendering��$ 	IsEnabled�� -	   $��isPreviewing��{�`� #	 [ .1��� $
 notPreviewing��$ 	IsEnabled�� -
   $��stopEnabled��{�`�   OrValueConverter#	 [ .1��� $
 isRendering��$ 	IsEnabled��� $
 isPreviewing��$ 	IsEnabled�  Zenith_MIDI.CustomTabs  -     Name$ 
windowTabs��
������ $��0q�$��0,0,0,0q�����  �� 
TargetType"  .���  �� Property � �� �� Value ��  �� 
TargetType"  .R��  � RowDefinitions �� $��50 ��� $��* �.+��� $��titlebar��$��0��$	��Top=�$��50�����	#11FFFFFFy��� *ChromeVisibility+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL�._�]� .+��� $6��0pack://application:,,,/Zenith;component/icon.png�� �� BitmapScalingMode$ HighQuality=�$��30��$��30��$��10q��� $��Center=�$��20(�$��	-5,0,10,0q�����  �� *TemplatedParent+$ VersionName��� $��0�� K� ZIndex$ 1��$��0,0,0,0q� K� IsItemsHost . 
������ �� -   $��20,0,0,0q�$��Center=� � Cursor$ Arrow��   IsHitTestVisibleInChrome . ���� " ��.���  � y�$ 	Collapsed=� �� Triggers ��  �� Binding ��  �� *TemplatedParent+$ UpdaterProgress� �� Value 
Downloaded.a���  � y�$ Visible=�.+��� 
�������$��14(�  updateReady#���  �� 
�������$��14(�  clickToRestart#���  �� $��20,0,0,0q�$��Center=����� " ��.���  � y�$ 	Collapsed=� ��  ��  �� *TemplatedParent+$ UpdaterProgress� Downloading.a���  � y�$ Visible=�.+��� 
�������$��20(� 	 updateDownloading#��� 	 ]� $��Right=�$��Stretch=�$��Right=�$��0,0,10,0q�$��2��$��52��$	 100��.+��� $��
ExitButton��-    
 WindowButton#��[
 |�.  
���  ��$��20��$��20��$��3q�$��Right=� . �� $��MinimiseButton��-   #��[
 |�.  
��� ���$��20��$��20��$��3q�$��Right=� . �� $��1�� �� CornerRadius$ 0, 12, 12, 12��._��� $��SelectedContent��   MainTabs �� BasedOn��%c   '��       7  �� " ��8   .���  � ��$ 50����   �@ IsHitTestVisibleInChrome . .@���   
generalTab#���  #{��  .��� .+��� .+�� $��30��$��10q� � ColumnDefinitions �� �� .+��� -    ��Name$ browseMidiButton��$��0,0,3,0q�  load#���  $��	30,0,30,0q�$
��Left=�{�`� #	 [ .1��� $
 notPreviewingOrRendering��$ 	IsEnabled��� $
 midiNotLoaded��$ 	IsEnabled��� -   $ unloadButton��$��3,0,0,0q�$��1�� 
 unload#���  $��	20,0,20,0q�$
��Left=�{�`� #	 [ .1��� $
 notPreviewingOrRendering��$ 	IsEnabled��� $
 
midiLoaded��$ 	IsEnabled��� $��10q�.+��  �� �� .+���   �� Padding$  0q�._��� .+��� $��16(�$��Center=�  renderResolution#���  �  �� �� .+��� .+�]� ��.  $��0,0,0,5q�$
��Left=�.+���  	 width#���  $
��Left=�$	��Top=�$
��Left=�"  ZenithEngine.UI.NumberSelect  -   !  Name$! 	viewWidth��"   Maximum$" 15360~�#   Minimum$# 1~�$	��118��$
��Left=�$��10,0,0,0q�{��� *	IsEnabled+$
 notPreviewingOrRendering��]� ��.  $
��Left=�.+���  
 height#���  $
��Left=�$
��Left=�$��0,0,0,0q�  -   $! 
viewHeight��$
" 8640~�$# 1~�$	��118��$
��Left=�$��5,0,0,0q�{��� *	IsEnabled+$
 notPreviewingOrRendering��]� ��.  $	��Top=�$��0,5,0,0q�.+���   previewWidthLabel#���  $	��Top=�$��0,5,0,0q�$
��Left=�  -   $! previewWidthSelect��$	��Top=�$��10,5,0,0q�$
��Left=�$# 1~�$" 15360~�$��93��$   Value$
$ 1920~�$
��Left=�{��� $
 notPreviewingOrRendering��$ 	IsEnabled��� $��1��.+�]� ��.  $��0,0,0,5q�$
��Left=�.+���   presets#���  $
��Left=�$	��Top=��� -   % ��Name$% resolutionPreset��$��10,0,0,0q�$	��110��{��� $
 notPreviewingOrRendering��$ 	IsEnabled�.@��� $
��720p���� $��1080p���� $��1440p���� $��4k���� $��5k���� $��8k���� $	��16k��]� $	��Top=�$
��Left=���.  $	��0,5q�$	��Top=�.+���   superRes#���  $
��Left=�$	��Top=�$
��Left=�  -   $! 
SSAAFactor��$
��Left=�$$ 1~�$" 8~�$# 1~�$
��Left=�$	��118��$��20,0,0,0q�{��� *	IsEnabled+$
 notPreviewingOrRendering��]� .+���   previewHeightLabel#���  $	��Top=�$��0,0,0,0q�$
��Left=�  -   $! previewHeightSelect��$	��Top=�$��0,0,0,0q�$
��Left=�$# 1~�$
" 8640~�$��93��$
$ 1280~�$
��Left=�{��� $
 notPreviewingOrRendering��$ 	IsEnabled��� $  0q�$��1��._��� .+��� $��16(�$��Center=�  renderOptions#���  ]� $��Center=�$��0,0,0,10q�.+�]� ��.  $	��Top=�$
��Left=�.+���   fps#���  $
��Left=�& J� RenderTransformOrigin$& 
1.078,0.59&�$
��Left=�$��0,0,0,0q�  -   $! viewFps��$
" 1000~�$# 1~�$��77��$
��Left=�$��5,0,0,0q�{��� *	IsEnabled+$
 notPreviewingOrRendering��]� $��Center=���.  $��20,0,0,0q�$	��Top=�.+���   noteSize#���  $
��Left=�$	��Top=�$
��Left=��� -   $% noteSizeStyle��$
��Left=�$��7,4,10,4q�$��5,0,0,0q�' �� SelectedIndex$' 0��{��� $
 notPreviewingOrRendering��$ 	IsEnabled�.@���   	tickBased#���  ��   	timeBased#���  $  ZenithEngine.UI.BetterCheckbox  -   (  Name$( useMultithreadToLoadMidi��$��26��  useMultithreadToLoad)   Text#) �  $��Center=�  -   $( ignoreColorEvents��$��26��  ignoreColorEvents#) �  $��Center=�]� ��.  $	��Top=�.+��� $	��Top=�$
��Wrap=�  zenithRenderWarning#���  $	��Top=�$��5,5,0,5q�$
��Left=��� $	��Top=�$
��Wrap=�  superResWarning#���  $	��Top=�$��5,5,0,5q�$
��Left=�]� ��. $��26��$��	0,10,0,20q�$	��Top=�.+�  -   $( 
useBGImage��  useBackgroundImage#) �  $
��Left=�$
��Left=�$��0,4,0,0q��� -   $ browseBG�� 
  browse#���   $��	15,1,15,1q�$
��Left=�$
��Left=�$��5,0,0,0q�{��� $
 
useBGImage��$ 	IsChecked��� -   * ��Name$* bgImagePath��$�� ��$��Right=�+ �� TextWrapping$
+ Wrap=�$��Stretch=�$��5,0,0,0q�{�.  �� $��16(�$��Center=� ! previewOptions#��� ! ]� $��Center=�$��30����.  $��0,0,0,5q�$	��Top=�.+���  	" tempo#��� " $
��Left=�$	��Top=�$
��Left=�!	  ZenithEngine.UI.ValueSlider	  -   $��tempoMultSlider��$	��396��, 	  Minimum$, -4Q�- 	  Maximum$- 6Q�. 	  TrueMin$
. 0.01~�/ 	  TrueMax$
/ 1000~�0 	  DecimalPoints$0 2��1 	  Value$1 1Q�$��0,0,0,4q�]� $��Center=���.  $	��Top=�$��30��.+���  # 	skipLabel#��� # $
��Left=�$
��Left=�	  -   $��	skipValue��$	��396��$	, 100Q�$- 100000Q�$	. 100~�$/ 100000~�$0 0��$
1 5000Q�$��0,0,0,5q�� $��0,5,0,0q� �� �� �� .+�  -    $( vsyncEnabled��$��Center=� 	$ vsync#) � $ |�.  2   	IsChecked2 .   -!   $( previewPaused��$��Center=�$��1�� 
% paused#) � %   -"   $��realtimePlayback��$��Center=�$��2�� & realtimePlayback#) � & 2 . ]� $��10,10,10,10q�.+��� -#   $ disableKDMAPI��$
��Left=� ' disableKDMAPI#��� ' $��	20,2,20,2q��� -$   $ reloadKDMAPI��$
��Left=� ( reloadKDMAPI#��� ( $��	20,2,20,2q�$��15,0,0,0q���  ) pauseHintNew#��� ) $
��Left=�$��10,0,0,0q�$	��Top=�]� ��.  $��	0,0,10,10q�$��Right=�$	��293��$��30��$��Bottom=�.+��� -%   $% languageSelect��$��Right=�$
��Left=�$��0q�$	��120���� $��Right=� * language#��� * $	��Top=�]� $
��Left=���.  $��	10,0,0,10q�$��30��$��Bottom=�.+��� -&    + startPreview#��� + $
��Left=�$��0,0,0,0q�$��	20,0,20,0q�$
��Left=�$��26��{��� *	IsEnabled+$
 previewStartEnabled���� -'    , stop#��� , $
��Left=�$��10,0,0,0q�$��	20,0,20,0q�$
��Left=�$��26��{��� *	IsEnabled+$
 stopEnabled����  - 
modulesTab#��� - #{��  .��]� $��10q�.+��� -(   $��pluginDescription��$��Bottom=�$��0,10,0,0q�$	��100��3 �� TextAlignment$3 Center=�$��1��$
+ Wrap=�{�.  ]� .+�]� $	��170��.+��� -)   $��reloadButton��$	��Top=� 
. reload#��� . $��0,0,0,0q�$��26���� -*   $��pluginsList��$��0,10,0,0q��� -+   $��previewImage��$��10,0,0,0q�$��Uniform=���  / moduleSettingsTab#��� / #{��  .��� -,   $��pluginsSettings����  0 	renderTab#��� 0 $��0q�.��]� $��-1,-2,-1,-1q�.+��� $��0,3,0,0q�.@���  1 BasicSettingsTab#��� 1 $��36�����#171717.��� ���#171717$��-5,-3,-5,-5q�.+��� $��5,5,5,0q����#171717.+�]� $��26����. $��0,5,0,0q�$	��Top=�.+��� $��0,0,0,0q� 2 savePath#��� 2 �� --   $ browseVideoSaveButton��#���   $
��Left=�$��10,0,0,0q�$��	20,0,20,0q�#{��  �� -.   $* 	videoPath��$��10,0,0,0q�{�.  $
+ Wrap=�$�� ��]� $��26����. $��0,10,0,0q�$	��Top=�.+�  -/   $( includeAudio��$
��Left=� 3 includeAudio#) � 3 $
��Left=�#{��  �� -0   $ browseAudioButton��$
��Left=�#���   $
��Left=�$��10,0,0,0q�$��	20,0,20,0q�{�`� #	 [ .1��� $
 includeAudio��$ 	IsChecked��� $
 notRendering��$ 	IsEnabled��� -1   $* 	audioPath��$
��Left=�$��10,0,0,0q�{�.  $
+ Wrap=�$�� ��]� $��26����. $��0,10,0,0q�$	��Top=�.+�  -2   $( includeAlpha�� 4 
renderMask#) � 4 $
��Left=�#{��  $
��Left=��� -3   $ browseAlphaButton��#���   $
��Left=�$��	20,0,20,0q�$
��Left=�$��10,0,0,0q�{�`� #	 [ .1��� $
 includeAlpha��$ 	IsChecked��� $
 notRendering��$ 	IsEnabled��� -4   $* 	alphaPath��{�.  $
+ Wrap=�$�� ��$��Right=�$��10,0,0,0q�]� $��30����.  $��3,30,0,0q�$	��Top=�.+�!
  ZenithEngine.UI.BetterRadio
  -5   4 
 Name$4 bitrateOption��#{��  5 
  ParentDepth$5 3�� 5 bitrate6 
  Text#6 � 5 $
��Left=�$	��Top=�7 
  	IsChecked7 . $
��Left=�$��0,6,0,0q�  -6   $! bitrate��$" 2000000~�$# 1~�$$ 20000~�$
��Left=�$	��118��$
��Left=�$��10,0,0,0q�{�`� #	 [ .1��� $
 bitrateOption��$ 	IsChecked��� $
 notRendering��$ 	IsEnabled��� $
��kbps��$
��Left=�$	��Top=�$
��Left=�$��5,0,0,0q�]� ��.  $��3,10,0,0q�$	��Top=�.+�
  -7   $4 	crfOption��$5 3��#{��  $	6 crf��$
��Left=�$& 5.125,1.133&�$
��Left=�  -8   $! 	crfFactor��$$ 17~�$" 51~�$# 0~�$
��Left=�$��64��$
��Left=�$��10,0,0,0q�{�`� #	 [ .1��� $
 	crfOption��$ 	IsChecked��� $
 notRendering��$ 	IsEnabled��� -9   $% 	crfPreset��$
��Left=�$
��Left=�$��10,0,0,0q�$	��122��$' 5��{�`� #	 [ .1��� $
 	crfOption��$ 	IsChecked��� $
 notRendering��$ 	IsEnabled�.@��� $��	ultrafast���� $��	superfast���� $��veryfast���� $��faster���� $
��fast���� $��medium���� $
��slow���� $��slower���� $��veryslow����  6 crfHint#��� 6 $
��Left=�$
��Left=�$��10,0,0,0q�]� $��30����.  $��3,10,0,0q�$	��Top=�.+�
  -:   $4 FFmpeg��#{��  $5 3�� 
7 Custom#6 � 7 $
��Left=�$	��Top=�7 .  $
��Left=�$��0,6,0,0q�]� .+��� -;   $* FFmpegOptions��8 �� AcceptsReturn8 . $	��Top=�$��20,5,0,0q�$
+ Wrap=�$��50��$��FFmpeg options��.��{�`� #	 [ .1��� $
 FFmpeg��$ 	IsChecked��� $
 notRendering��$ 	IsEnabled�.��]� .+���  8 
delatStart#��� 8 $
��Left=�$	��Top=�$
��Left=�$��0,15,0,0q�  -<   $! secondsDelay��$$ 0~�$" 10~�$# 0~�$
��Left=�$��82��$
��Left=�$��0,10,0,0q�$��31��{��� *	IsEnabled+$
 notRendering��]� $
��Left=�$��26����.  $��0,10,0,0q�.+��� -=   $ startRenderButton�� 9 startRender#��� 9 $
��Left=�$��	20,0,20,0q�$
��Left=�{��� *	IsEnabled+$
 previewStartEnabled���� ->   #��� , $
��Left=�$��	20,0,20,0q�$
��Left=�$��15,0,0,0q�{��� *	IsEnabled+$
 stopEnabled��  -?   $( ffdebug��#{��   : ffmpegDebug#) � : $
��Left=�$
��Left=�$��10,0,0,0q���  ; ifUploading#��� ; $
��Left=�$��Bottom=�$��0,10,0,0q��� -@   $* 
creditText��$
��hhhh��$
��Left=�$��Bottom=�$��5,5,0,0q�$	��331��{�.  ��  < OtherSettingsTab#��� < $��36�����#171717.��� $��-5,-3,-5,-5q����#171717.+��� ���#171717$��5q�.+�]� ��.  $��0,10,0,0q�$	��Top=�.+�  -A   $( enableMaxMemory�� = 	maxMemory#) � = $
��Left=�$	��Top=�$
��Left=�$��0,7,0,0q�	  -B   9 	 Name$9 maxRenderMemory��$
, 2048Q�$- 1048576Q�$
��Left=�$	��Top=�$	��312��$��15,0,0,0q�$
1 8192Q�: 	  Step$: 1~�$0 0��$/ 1048576~�$
. 2048~�{�`� #	 [ .1��� $
 enableMaxMemory��$ 	IsChecked�]� .+�  -C   $ ( collectMemoryWithCondition�� > collectMemoryIf#) � > $��	0,10,0,10q�$
��Left=�$
��Left=�$	��Top=�	  -D   $ 9 collectNotesDrawnCondition��$, 100000Q�$- 10000000Q�$/ 10000000~�$. 100000~�$
��Left=�$	��322��$��15,5,0,0q�$	��Top=�$: 10000~�$1 1500000Q�{�`� #	 [ .1��� $ 
 collectMemoryWithCondition��$ 	IsChecked�]� .+�  -E   $ ( autoAdjustMemoryCollection�� ? adjustAutomatically#) � ? 2 . $
��Left=�$	��Top=�$��0,0,0,10q�]� .+�  -F   $( enableMemorySaving�� @ enableMemorySaving#) � @ $
��Left=�$
��Left=�$��0,0,0,0q�  -G   $( autoDisableKDMAPI�� A autoDisableKDMAPI#) � A $
��Left=�$	��Top=�$
��Left=�$��0,10,0,0q�2 .  {��� *	IsEnabled+$
 notRendering��  -H   $( 
doNotWrite�� B 
doNotWrite#) � B $
��Left=�$	��Top=�$��	0,10,0,10q�2 .  �� -I    C manualCollect#��� C $
��Left=�$	��Top=�$��0,0,0,10q�$��26��$	��180���� -J    D clearTheConsole#��� D $
��Left=�$	��Top=�$��0,0,0,10q�$��26��$	��180��]� ��.  $��0,25,0,5q�$	��Top=�.+�  -K   $( enableFilterArg�� E filterThreadsUsing#) � E $
��Left=�$	��Top=�$
��Left=�$��0,0,0,0q�2 .  {��� *	IsEnabled+$
 notRendering��  -L   $! filterThreadsForRender��$$ 8~�$	" 128~�$# 1~�$
��Left=�$��82��$
��Left=�$��5,-8,0,0q�{�`� #	 [ .1��� $
 notRendering��$ 	IsEnabled��� $
 enableFilterArg��$ 	IsChecked�]� ��.  $��	0,10,0,10q�.+���  F encoders#��� F $
��Left=�$
��Left=��� -M   $% encoderSelect��$
��Left=�$	��122��$' 0��$
��Left=�$��10,0,0,0q�{��� *	IsEnabled+$
 notRendering��.@��� $��libx264���� $��
libx264rgb���� $��
h264_nvenc���� $��libx265���� $��
hevc_nvenc���� $��libxvid���� $	��png��]� .+���  G yuv#��� G $
��Left=�$
��Left=�$��0,0,0,0q��� -N   $% 	yuvSelect��$
��Left=�$
��Left=�$��10,0,0,0q�$	��122��$' 1��$��31��{��� *	IsEnabled+$
 notRendering��.@��� $
��411p���� $
��420p���� $
��422p���� $
��444p��]� .+�  -O   $( 	fullFFArg�� H 	fullFFArg#) � H $
��Left=�$
��Left=�$��	0,10,0,10q�2 .  {��� *	IsEnabled+$
 notRendering��]� .+��� -P   $* fullFFmpegArg��$
��Left=�$)��# -f rawvideo -vf vflip -pix_fmt rgb��$��20,0,0,0q�$	��Top=�$��70��8 . $
+ Wrap=�.��{�`� #	 [ .1��� $
 	fullFFArg��$ 	IsChecked��� $
 notRendering��$ 	IsEnabled�.����  I 
renderInfo#��� I $��36�����#171717.��� $��-5,-3,-5,-5q����#171717 �� $	��900 �.+��� $��5q�$��0��.+�]� $��0,0,0,0q�$	��Top=�.+���  J zenithRenderInfo#��� J $��18(�]� $��0,10,0,0q�$	��Top=�.+���  K renderProgress#��� K $	��200��$
��Left=�$��16(��� -Q   ; ��Name$; renderProgress��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  L 	renderFPS#��� L $	��200��$
��Left=�$��16(��� -R   $; 	renderFPS��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  M renderSpeed#��� M $	��200��$
��Left=�$��16(��� -S   $; renderSpeed��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  N renderedFrame#��� N $	��200��$
��Left=�$��16(��� -T   $; renderedFrame��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  O 
notesDrawn#��� O $	��200��$
��Left=�$��16(��� -U   $; 
notesDrawn��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  P 	avgMemory#��� P $	��200��$
��Left=�$��16(��� -V   $; averageMemory��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  Q 
peakMemory#��� Q $	��200��$
��Left=�$��16(��� -W   $; maximumMemory��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  R 
currMemory#��� R $	��200��$
��Left=�$��16(��� -X   $; currentMemory��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  S timeToRender#��� S $	��200��$
��Left=�$��16(��� -Y   $; 
renderTime��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  T avgSpeed#��� T $	��200��$
��Left=�$��16(��� -Z   $; avgSpeed��$��-��$
��Left=�$��16(�]� $��0,10,0,0q�$	��Top=�.+���  U renderStatus#��� U $	��200��$
��Left=�$��16(��� -[   $; 
currStatus��$��-��$
��Left=�$��16(�