export OUT_DIR="/mnt/c/Program Files (x86)/Steam/steamapps/common/A Dance of Fire and Ice/BepInEx/plugins/AdofaiUtils"

rm -rf "$OUT_DIR"

mkdir -p "$OUT_DIR"

cp AdofaiUtils2/bin/Debug/* "$OUT_DIR"
