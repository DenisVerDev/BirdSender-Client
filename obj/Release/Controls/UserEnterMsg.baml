   M S B A M L   `   `   `  ���� J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  +    %MessangerClient.Controls.UserEnterMsg   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:MessangerClient.Controls     b ^SharpVectors.Converters.Wpf, Version=1.6.0.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgc&http://sharpvectors.codeplex.com/svgc/  $��60��  � 	Resources     smic �� 
IsAdditive	 �� To g� Duration �� 
TargetName �� TargetProperty W� From	 W� To g� AutoReverse	 g� RepeatBehavior
 t� 
IsAdditive	 t� To �� 
TargetType  btnfile ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value(  "SharpVectors.Converters.SvgViewbox   Source 
 btnmic �� CornerRadius��%t  &	        &	 F    &	     �� .����  . $ #6CC4C7X�$ 0:0:0.5D�$
 body��$ Background.Color�W� $ 40X�$ 50X�$ 0:0:0.5D� . $	 Forever��$
 body��$ Width�W� $ 40X�$ 50X�$ 0:0:0.5D� . $	 Forever��$
 body��$ Height�t� 
 . $	 -10X�$ 0:0:0.5D�$	 Forever�� . $
 body��$ Margin��� " ��.R��� $
 body��
������ ._��� $ content��#��z�� P�  �@J�IsMouseOver . . ��� $ content�� � ��   $��28��$��28��$ ../Resources/btnfile2.svg?��� " ��.R��� $
 body��
������ $ 50��._��� $ content��#��z��.��� ���#E6E6E6 � ColumnDefinitions �� $��60 ��� $��* ��� $��60 �.+��� -    ��Name$ btnfiles��#��[ $��0��$��Center=���  $��28��$��28��$ ../Resources/btnfile.svg?�*   $MessangerClient.Controls.BirdTextBox  -     Name$ tbmsg��*   $MessangerClient.Controls.BirdTextBox   KeyUp$ tbmsg_KeyUp��   	MaxLength$	 300��$��1��$
��Auto��$��50��$
��10pt(�   DefaultText$ Write a message...���� -   $ btnmic��#��[ $��40��$��40��$��2��$��Center=���  $��28��$��28��$ ../Resources/btnmic.svg?�