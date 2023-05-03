
    export class ChatRoomModel
    {
      public selected?= false;

      // these are database mapped, come from the API calls
      public id: string = '';
      public users: string[] = [];
      public admins: string[] = []
      public chatName: string = '';

      constructor(init?: Partial<ChatRoomModel>) { // allows us to assign this object C# style
        Object.assign(this, init); // copies all the properties of 'init' that exist in the 'this' object
      }
    }

