   M S B A M L   `   `   `  ����G  CZenithEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ZenithEngine.UIZenithEngine.UI  5      "    ZenithEngine.UI.NumberSelect   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:ZenithEngine.UI     5      -   5        &   System.Windows.Media.TextOptions    TextRenderingMode$  Aliased=�5       � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Material.xaml?�6%   %    5      .��5      � }�.  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��25 �6   .+�5   
   � $��0,0,3,0q�6   .+�5      �� -   5         ��Name$ textBox��5   *   }�. $��0,1,0,0q�6;    �� TextWrapping$
 Wrap=�6M   
������ $��0q�5      
�������$��0��5       �� 
CaretBrush
 ����� J� RenderTransformOrigin$ 0.5,0.5&�5      $��16(�6�   $��0,2,0,0q�6�   .��6�   ��5      �� 5       	 �� 
TargetType"	 ��6   .�5!      �� 
 �� Property
 � �� �� Value �#66666667   �� 5"      
 � ��
 �����67   �� 5#      
 � ��$ Center=�6E   �� 5$      
 � ��$ 3q�64   �� 5%      
 � �� 5&      �� 5'   "    �� 
TargetType" ��62   .R�5(   &   �� $��Bd��6-   #��z��67    J� SnapsToDevicePixels . ._�5*   *   �� $��PART_ContentHost��67   # z 5+   +   5,   '   5-   #   5/      50      .��51   !   z�52      V� 53      .~�54      �� �� 55       �� AngleX$ -0.843Q�6(   �� 56      T� 57       T� X$ -0.235Q�6-   58      .��59   +   5:      �� 5;         PrimaryBrush#��[  6   $��1��6=   $��Bottom=�6H   5<      � 5=   
   $��1��6   $��25��6   $��25��6*   $��2��66   $	��0,0q�6J    � RowDefinitions 5>      �� 5?      �� 5@      .+�5B      �� -   5        $��	downArrow��5B      $��12(�6&   $��1��64   $��	0,0,0.3,0q�6A   ���	#33FFFFFF$
��Auto��6l   $��0q�6z   ��5C      �� 5D      
�������$��0,0,0,0q�6/   $��0,-0,0,0q�6A   $
��Bold"�6S   .��6e   ﹀6h   5F      �� 5G      -   5        $��upArrow��5G      $��12(�6$   $��	0,0,0.3,0q�62   ���	#33FFFFFF$
��Auto��6]   $��0q�6k   ��5H      �� 5I      
�������$��0,0,0,0q�6/   $��0,-6,0,0q�6A   $
��Bold"�6S   .��6e   ︿6h   5K      5L      5M      5N      