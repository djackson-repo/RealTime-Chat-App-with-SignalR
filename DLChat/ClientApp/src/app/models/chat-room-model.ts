
    export class ChatRoomModel
    {
      public selected?= false;

      // these are database mapped, come from the API calls
      public Id: string = '';
      public password: string = '';
      public name: string = '';

      constructor(init?: Partial<ChatRoomModel>) { // allows us to assign this object C# style
        Object.assign(this, init); // copies all the properties of 'init' that exist in the 'this' object
      }
    }

