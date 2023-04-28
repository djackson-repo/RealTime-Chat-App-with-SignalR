
    export class ChatMessageModel
    {
      public selected?= false;

      // these are database mapped, come from the API calls
      public Id: string = '';
      public userId: string = '';
      public chatRoomId: string = '';
      public message: string = '';

      constructor(init?: Partial<ChatMessageModel>) { // allows us to assign this object C# style
        Object.assign(this, init); // copies all the properties of 'init' that exist in the 'this' object
      }
    }

