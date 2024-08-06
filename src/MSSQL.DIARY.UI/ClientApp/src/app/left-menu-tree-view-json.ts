import { SchemaEnums } from 'src/models/util/schema-enums';

export class LeftMenuTreeViewJson {
    text: string
    icon : string;
    mdaIcon : string;
    link: string;
    selected: string;
    badge: string;
    expand: string;
    leaf: string;
    SchemaEnums: SchemaEnums;
    children: string[]; 
}
