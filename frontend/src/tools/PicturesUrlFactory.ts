import config from "../../config.json"
export default function PictureUrl(fileName: string) {
    return `${config.PICTURES_STORAGE_URL}/${fileName}`;
}