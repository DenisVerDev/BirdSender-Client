   M S B A M L   `   `   `  ����J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  5      +    %MessangerClient.Controls.UserEnterMsg   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:MessangerClient.Controls     5      b ^SharpVectors.Converters.Wpf, Version=1.6.0.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgc&http://sharpvectors.codeplex.com/svgc/  5      $��60��5	   7     � 	Resources  5
         smic �� 
IsAdditive	 �� To g� Duration �� 
TargetName �� TargetProperty W� From	 W� To g� AutoReverse	 g� RepeatBehavior
 t� 
IsAdditive	 t� To �� 
TargetType  btnfile ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value(  "SharpVectors.Converters.SvgViewbox   Source 
 btnmic �� CornerRadius��%  &	        &	 �    &	 ]    �� 5   
   .��5      ��  . $ #6CC4C7X�6/   $ 0:0:0.5D�6<   $
 body��6O   $ Background.Color�6l   6�   W� 5      $ 40X�6   $ 50X�6(   $ 0:0:0.5D�60    . $	 Forever��6W   $
 body��6p   $ Width�6�   6�   W� 5      $ 40X�6   $ 50X�6(   $ 0:0:0.5D�60    . $	 Forever��6V   $
 body��6o   $ Height�6�   6�   t� 5      
 . $	 -10X�63   $ 0:0:0.5D�6<   $	 Forever��6O    . $
 body��6{   $ Margin�6�   6�   5      �� 5   
   " ��6   .R�5      �� $
 body��6   
������ ._�5      �� $ content��6#   #��z��64   6Z   5       5      P� 5       �@J�IsMouseOver . . �5      �� $ content��60    � �� 5        5      $��28��6.   $��28��69   $ ../Resources/btnfile2.svg?�6E   5      5       5!      5#      �� 5%   
   " ��6   .R�5&      �� $
 body��6   
������ $ 50��6<   ._�5'      �� $ content��6#   #��z��64   6Z   5(      5)      5+      .��5,      � ���#E6E6E6 � ColumnDefinitions 5-   
   �� 5.      $��60 �6   6,   �� 5/      $��* �6   6+   �� 50      $��60 �6   6,   .+�52   
   �� -   5         ��Name$ btnfiles��52   6   #��[ 6   $��0��6H   $��Center=�6X   ��53        54      $��28��6"   $��28��6-   $ ../Resources/btnfile.svg?�69   55      57      *   $MessangerClient.Controls.BirdTextBox  58   
   -   5          Name$ tbmsg��58      *   $MessangerClient.Controls.BirdTextBox   KeyUp$ tbmsg_KeyUp��6�      	MaxLength$	 300��6+   $��1��6;   $
��Auto��6K   $��50��6X   $
��10pt(�6d      DefaultText$ Write a message...��6t   6�   �� 59   
   -   5        $ btnmic��59   K   #��[ 6   $��40��64   $��40��6@   $��2��6[   $��Center=�6k   ��5:        5;      $��28��6"   $��28��6-   $ ../Resources/btnmic.svg?�69   5<      5>      5?      5@      