export default function PictureUrl(fileName?: string) {
    if (fileName === undefined)
        return "";
    
    return `${'https://dddstoragedemo.blob.core.windows.net/pictures-ddd-container'}/${fileName}`;
}
