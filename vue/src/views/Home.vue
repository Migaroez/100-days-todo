<template>
  <div class="home">
    <div>
      <p v-for="(item,index) in items" :key="index">{{item.description}}</p>
    </div>
    <div>
      <input name="newText" 
        title="Enter item to add"
        v-model="newDescription"/>
      <button type="submit" v-on:click="AddItem">Add</button>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import itemService from "@/services/itemService.js"

export default {
  name: 'Home',
  data: function(){
    return {
      items: [],
      newDescription: ""
    }
  },
  created(){
    // get data
    this.GetItems();
  },
  methods : {
    async GetItems(){
      var result = await itemService.GetList();
      this.items = result.value;
    },
    async AddItem(){
      var addResult = await itemService.Add(this.newDescription);
      if(addResult.success){
        this.newText = "";
        this.GetItems();
      }
    }
  }
}
</script>
