
mkdir tmp

cd tmp

mkdir AdofaiUtils

cd AdofaiUtils

mkdir lib

cd ..

copy ..\AdofaiUtils\bin\Debug\AdofaiUtils.dll AdofaiUtils\AdofaiUtils.dll

tar -a -c -f "C:\Program Files (x86)\Steam\steamapps\common\A Dance of Fire and Ice\Mods\AdofaiUtils.zip" AdofaiUtils

cd ..

rmdir /s /q tmp

explorer steam://rungameid/977950

echo exit
