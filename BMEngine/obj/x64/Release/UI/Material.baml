   M S B A M L   `   `   `  ���� G  CZenithEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ZenithEngine.UIZenithEngine.UI  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem b ^PresentationFramework.Aero2, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source �� 
TargetType �� Property �� Value   WindowButton �� 
TargetType �� Padding �� CornerRadius �� Triggers	 P� Property
 P� Value �� 
TargetName  MenuButtonStyle  MainTabs+    %ZenithEngine.UI.RippleEffectDecorator    
ExpandTime    FadeTime ��Name  SubTabItems  SubTabs �� RelativeSource �� AncestorType K� ZIndex K� IsItemsHost ��Name  PrimaryColor �� Color J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   	Direction   
BlurRadius N� EnterActions �� 
TargetName �� TargetProperty g� Duration	 �� To	 W� To N� ExitActions  ComboBoxToggleButton  J� SnapsToDevicePixels! F�Name" �� Value# �� Binding$ `� 	Converter&    ZenithEngine.UI.OrValueConverter% �� Path& �Name' � ColumnDefinitions( �Name) � AllowsTransparency* �� Mode+ �� Mode, ��Name- �� ClearTypeHint. ��Name/ ��Name0 �� ElementName1 ��Name-  'System.Windows.Input.KeyboardNavigation2   DirectionalNavigation3 d�Name4 d� 	IsChecked5 �� SelectionBoxItemTemplate6 �� SelectionBoxItem#7 �� SelectionBoxItemStringFormat8 �� ContentStringFormat9 J� IsHitTestVisible: ]� 
Conditions; �� Property< �� Value= P� 
SourceName  FocusVisual> �� StrokeDashArray�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     'localclr-namespace:ZenithEngine.UI  .sys&clr-namespace:System;assembly=mscorlib  UthemeKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2  Imedia9http://schemas.microsoft.com/winfx/2006/xaml/presentation      �� $ Scrollbar.xaml?��� $ Colors.xaml?��� $ ListBox.xaml?�%}  '��       '�� ;     &	  v     &	     &	 r    &	 �    &	 {    '�� �    7  7  7  &	 �    '��      7  '�� �    '�� h    &	 �    �� " ��.���  � ��
 �������  � ��$ 14(��� " ��.���  � ��
 �������  � ��$ 14(��� " ��.���  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $��border��$��1q�$	 4,2q�
�������$ 3��#��z��._�� .+��� $��content��$��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ��� $ border�� � �� �	#FF4788c8��  � �� �	#FF4788c8P� 	 � ��
 . P� 	 �@��IsDefaulted
 . P� 	 �@J�	IsFocused
 . �� " ��.���  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $��border��$	 4,2q�#��z��._�� .+��� $��content��$��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ���  � �� �	#44FFFFFFP� 	 � ��
 . P� 	 �@��IsDefaulted
 . P� 	 �@J�	IsFocused
 . �� " ��.���  � ��
 �������  � ��$ 14(���  � ��$
 Left=���  � �� �� " ��.R�� $��Panel��.+�   $	 0.2Q�$	 0.2Q�.���� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,2q� P� 	 �@��
IsSelected
 . . ��� $ Panel�� � �� �	#33FFFFFFP� 	 �@��
IsSelected
 .  . ��� $ Panel�� � ��
 ���� �� " ��.���  � ��
 �������  � ��$ 14(���  � ��$
 Left=���  � �� �� " ��.R�� $��Panel��$��30��.+�   $	 0.2Q�$	 0.2Q�.���� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,2q� P� 	 �@��
IsSelected
 . . ��� $ Panel�� � �� �	#33FFFFFFP� 	 �@��
IsSelected
 .  . ��� $ Panel�� � ��
 ���� �� " ��.���  � ��
 ���� ��  � �� �� " ��.R�]� ��. .+��� $��titlebar��$��0��$	��Top=����	#11FFFFFFy��� *ChromeVisibility+ �� *FindAncestor+$ WindowL�._�]� .+��� $��0��$ 1��$��0,0,0,0q� . 
������ �� $��1��$ 0, 12, 12, 12��._��� $��SelectedContent���� " ��.���  �@�OverridesDefaultStyle . ��  � |� .  ��  � ��$ 14(���  � ��
 �������  � �� �� " ��.R�� #��z��#��z��.+��� $ 
background������ bgBrush��8      
dropShadow��$ 0Q�$ 0Q�$ 5Q�   $	 0.2Q�$	 0.2Q�.���� # z��._��� $��content��$��Center=�$��Center=� P� 	 �@J�IsMouseOver
 .  �� .���� .���� $ bgBrush��$ Color�$ 0:0:0.1D�$ #43A047X�W� $ 
dropShadow��$ 
BlurRadius�$ 0:0:0.1D�$ 10X� �� .���� .���� $ bgBrush��$ Color�$ 0:0:0.1D�8  W� $ 
dropShadow��$ 
BlurRadius�$ 0:0:0.1D�$ 5X�P� 	 � {�
 .   �� .���� .���� $ bgBrush��$ Color�$ 0:0:0.1D�$ #222222X�W� $ 
dropShadow��$ 
BlurRadius�$ 0:0:0.0D�$ 3X� �� .���� .���� $ bgBrush��$ Color�$ 0:0:0.1D�8  W� $ 
dropShadow��$ 
BlurRadius�$ 0:0:0.0D�$ 5X��� " d�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  �@��	ClickMode$ Press=���  � �� �� " d�.R�� .+��� 
���   �   
dropShadow��$ 0Q�$ 5Q��� $ templateRoot��  . ���� 
solidBrush��$ #222222��._��� $ splitBorder��
������ $��1q�$��Right=�$��0q�  . #��� r�._�F� $! arrow��F�����{��    ���( ��( ��o     ��o P����̰( � р{��P���р{��    ���#2E7D32$��Center=�$��0q�$��Center=� �� $
" True��# `� $   .1��� $% IsMouseOver� �� *TemplatedParent+ �� .���� .���� $ 
solidBrush��$ Color�$ 0:0:0.1D�$ #292929X�W� $ 
dropShadow��$ 
BlurRadius�$ 0:0:0.1D�$ 10X� �� .���� .���� $ 
solidBrush��$ Color�$ 0:0:0.1D�$ #222222X�W� $ 
dropShadow��$ 
BlurRadius�$ 0:0:0.1D�$ 5X��� " ��.���  � ��$ 14(���  � ��
 �������  � �� �� " ��.R�� $& templateRoot��  . ' �� $��* ��� #��� r�$��0 �.+�� $( 
PART_Popup��) . $��2��$��1q�#��� .�$��Bottom=����� *IsDropDownOpen+$* TwoWay=� �� *TemplatedParent+.��� ����  �� $+ TemplatedParent=�$% ActualWidth�.+��� 
���   �   $ 0Q�$ 5Q��� $ dropDownBorder��._��� $, DropDownScrollViewer��.��� $
& grid��$- Enabled=�.+��� $. canvas��$
��Left=�$��0��$	��Top=�$��0��.+��� $/ 
opaqueRect������ *
Background+$0 dropDownBorder������ *ActualHeight+$0 dropDownBorder������ *ActualWidth+$0 dropDownBorder���� $1 ItemsPresenter��$2 	Contained=�#  z  d� $3 toggleButton��#��z��#��z��#��z��$��2��8��  4 �� *IsDropDownOpen+$* TwoWay=� �� *TemplatedParent+�� $ contentPresenter��#��z5 #��z��#��z6 #8 z7 #��z��9 .  #��z��#  z  #��z�� P� 	 �@��HasItems
 .  . ��� $ dropDownBorder�� � ��$ 95��]� : �� ; �@��
IsGrouping< . �� "; �@%�IsVirtualizingWhenGrouping< .  ./���  � �� .  P� $= DropDownScrollViewer��	 � ��
 .  . ��� $ 
opaqueRect�� �� *VerticalOffset+$0 DropDownScrollViewer�� �@��Top�� $ 
opaqueRect�� �� *HorizontalOffset+$0 DropDownScrollViewer�� �@��Left�� " ��.���  � ��
 �������  � ��$ 14(���  � ��$ 5q���  � �� �#292929��  � �� �� " ��.R��� #��z��# z��._���  P� 	 �@J�IsMouseOver
 . . ���  � �� �#444444�� " ��.���  � �� �#444444��  � ��
 �������  � ��$ Center=���  � ��$ 3q���  � �� �� " ��.R�� .+��� 
���   �   
dropShadow��$ 0Q�$ 5Q��� $��Bd��#��z��#��z��  . ._��� $��PART_ContentHost��#��z��#  z   P� 	 � {�
 .  . ���  � �� �#333333]� : �� ; � {�< . �� ; �@J�IsMouseOver< .  �� .���� .��W� $ 
dropShadow��$ 
BlurRadius�$ 0:0:0.1D�$ 10X� �� .���� .��W� $ 
dropShadow��$ 
BlurRadius�$ 0:0:0.1D�$ 5X��� .���  � �� �� .R��� $��2q�  . #��� ��$��1��$	> 1 2R�